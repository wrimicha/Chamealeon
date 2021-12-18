using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.DTOs;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Helpers;
using ChamealeonApp.Models.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChamealeonApp.Controllers
{
    //Authors: 
    //Burhan (Implemented generating the meal plan and getting a meal full details from the Spoonacular API, updating meal plan with a user created meal
    //Mike
    //This controller is responsible for managing a meal plan
    [ApiController]
    [Route("api/[controller]")]
    public class MealPlanController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public MealPlanController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        //Author: Burhan
        //Generate a weekly meal plan (calls helper API)
        [Authorize]
        [HttpPost("generateMealPlan")]
        public async Task<IActionResult> GenereateMealPlan([FromBody] MealPlanQueryDTO mealPlanQuery)
        {
            try
            {
                //get the logged in user 
                var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).Include(u => u.PersonalNutritionalInformationGoal).FirstOrDefaultAsync(us => us.NormalizedEmail
                .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //call helper to make a request to API
                var retrievedRootResponse = await SpoonacularAPIHelper.GenerateMealPlanFromSpoonacularAsync(loggedInUser.Diet.Trim(), mealPlanQuery.ItemsToExclude, loggedInUser.PersonalNutritionalInformationGoal.Calories);

                //take the API response and convert to a proper meal plan
                var convertedMealPlan = await MealPlanResponseHelper.ConvertRootDTOToMealPlanAsync(retrievedRootResponse, _context);

                //add the meal plan to the db and to the user
                loggedInUser.CurrentMealPlan = convertedMealPlan;
                await _context.SaveChangesAsync();

                return Ok(convertedMealPlan);
            }
            catch (System.Exception)
            {

                return BadRequest(new ErrorDTO { Title = "An error has occured generating the meal plan." });
            }

        }


        //Author: Burhan
        //Update a specific meal in the weekly meal plan with a meal made by the user


        [Authorize]
        [HttpPut("updateMealPlanWithUserMeal/{id}")]

        //day of week is an enum, 0=Sunday
        //each day has a list of 3 meals, indices are 0,1 and 2
        //NOTE: Realized last minute that the database does NOT store the meals in the order of the index, it is most likely stored by id
        //This may or may not update the meal plan correctly but not enough time was left to determine the real reason

        public async Task<IActionResult> UpdateMealPlanWithUserMeal(string id, DayOfWeek day = DayOfWeek.Sunday, int mealIndexInDay = 0)
        {
            try
            {
                if (mealIndexInDay > 2 || mealIndexInDay < 0)
                {
                    return BadRequest(new ErrorDTO { Title = "Invalid meal selected to update." });
                }

                //get the logged in user 
                var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays.OrderBy(md => md.Day)).ThenInclude(md => md.Meals).FirstOrDefaultAsync(us => us.NormalizedEmail
                .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //find the meal in the database that they want to replace with, the spoonacular id should be empty if its a user defined meal
                var mealInDb = await _context.Meals.Where(m => m.SpoonacularMealId == 0).FirstOrDefaultAsync(userMeals => userMeals.Id.Equals(new Guid(id.Trim())));

                //replace the user meal with the new meal by getting the meal plan, get the day of the week, get the meal object in the list of meals for that day
                var mealToReplace = loggedInUser.CurrentMealPlan.MealDays.FirstOrDefault(m => m.Day == day).Meals[mealIndexInDay];
                mealToReplace = mealInDb;

                await _userManager.UpdateAsync(loggedInUser);

                //show the updated meal for that day
                return Ok(loggedInUser.CurrentMealPlan.MealDays.FirstOrDefault(m => m.Day == day).Meals.ToList());
            }
            catch (System.Exception)
            {

                return BadRequest(new ErrorDTO { Title = "An error has occured updating the meal plan." });
            }
        }

        // Mike
        // PUT update a specific meal in the 
        //TODO: Test with postman once we have GET route for the weekly meal plan
        [Authorize]
        [HttpPut("updateMealPlanWithSpoonacularMeal")]
        public async Task<IActionResult> UpdateMealPlanWithSpoonacularMeal(int spoonacularId, DayOfWeek day, int mealIndexInDay)
        {

            //TODO: Gets the meal in the meal plan and recplaces it with the choosen meal

            //make sure it saves to the user
            //get the logged in user 
            var loggedInUser = await _userManager.Users
                                                 .Include(u => u.CurrentMealPlan)
                                                 .ThenInclude(m => m.MealDays)
                                                 .ThenInclude(md => md.Meals)
                                                 .FirstOrDefaultAsync(us => us.NormalizedEmail
                                                 .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //Use the helper function to create a meal object from the spoonacular meal id
            var spoonacularMeal = MealPlanResponseHelper.ConvertSpoonacularMealToFullMealAsync(spoonacularId, _context);

            //assign the loggedin user the converted meal
            loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals[mealIndexInDay] = await spoonacularMeal;

            await _context.SaveChangesAsync();

            return Ok(loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals.ToList());
        }

        //Mike
        //GET meal plan (DB)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMealPlanFromDb()
        {
            try
            {
                //get the user meals order by the day of the week
                var loggedInUser = await _userManager.Users
                                                    .Include(u => u.CurrentMealPlan)
                                                    .ThenInclude(m => m.MealDays.OrderBy(md => md.Day)) //.OrderBy(md=>md.Day) - Don't need this days already in order
                                                    .ThenInclude(md => md.Meals)
                                                    .ThenInclude(n => n.NutritionInfo)
                                                    .FirstOrDefaultAsync(us => us.NormalizedEmail
                                                    .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //get the user's meal plan
                var mealPlan = loggedInUser.CurrentMealPlan;

                return Ok(mealPlan);
            }
            catch
            {
                return BadRequest(new ErrorDTO { Title = "An error occured removing meal from meal plan" });
            }
        }

        //Mike
        //DELETE a meal from the meal plan
        [Authorize]
        [HttpDelete("removeMealFromMealPlan")]
        //user needs to pass int of enum of DaysOfWeek (ex 3 = wednesday)
        public async Task<IActionResult> DeleteMealFromMealPlan(DayOfWeek day = DayOfWeek.Sunday, int mealIndexInDay = 0)
        {
            try
            {
                //get the user meals order by the day of the week
                var loggedInUser = await _userManager.Users
                                                    .Include(u => u.CurrentMealPlan)
                                                    .ThenInclude(m => m.MealDays)
                                                    .ThenInclude(md => md.Meals)
                                                    .FirstOrDefaultAsync(us => us.NormalizedEmail
                                                    .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //find the meal in the mealplan
                //var mealToDelete = loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals[mealIndexInDay];
                var mealToDelete = loggedInUser.CurrentMealPlan.MealDays.FirstOrDefault(md => (((int)md.Day).Equals((int)day))).Meals[mealIndexInDay];

                //delete the meal
                loggedInUser.CurrentMealPlan.MealDays.FirstOrDefault(md => (((int)md.Day).Equals((int)day))).Meals.Remove(mealToDelete);

                //update the database        
                await _userManager.UpdateAsync(loggedInUser);

                return Ok(mealToDelete);
            }
            catch
            {
                return BadRequest(new ErrorDTO { Title = "An error occured removing meal from meal plan" });
            }

        }

    }
}