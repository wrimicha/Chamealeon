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
    //TODO: Seperate the file into 2 controllers
    //Burhan (Implemented generating the meal plan and getting a meal full details from the Spoonacular API, updating meal plan with a user created meal
    //Mike
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
        [HttpPut("updateMealPlanWithUserMeal")]

        //day of week is an enum, 0=Sunday
        //each day has a list of 3 meals, indices are 0,1 and 2
        public async Task<IActionResult> UpdateMealPlanWithUserMeal(string mealId, DayOfWeek day = DayOfWeek.Sunday, int mealIndexInDay = 0)
        {
            try
            {
                if (mealIndexInDay > 2 || mealIndexInDay < 0)
                {
                    return BadRequest(new ErrorDTO { Title = "Invalid meal selected to update." });
                }

                //get the logged in user 
                var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).FirstOrDefaultAsync(us => us.NormalizedEmail
                .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //find the meal in the database that they want to replace with, the spoonacular id should be empty if its a user defined meal
                var mealInDb = _context.Meals.Where(m => string.IsNullOrEmpty(m.SpoonacularMealId.ToString()) == true)
                .FirstOrDefaultAsync(userMeals => userMeals.Id.Equals(new Guid(mealId.Trim())));

                //replace the user meal with the new meal by getting the meal plan, get the day of the week, get the meal object in the list of meals for that day
                //because each enum value is also the index in the list, we can cast the enum to an int (ex. 0 = Sunday)
                loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals[mealIndexInDay] = await mealInDb;

                await _context.SaveChangesAsync();

                //show the updated meal for that day
                return Ok(loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals.ToList());
            }
            catch (System.Exception)
            {

                return BadRequest(new ErrorDTO { Title = "An error has occured updating the meal plan." });
            }
        }

        //Mike
        //PUT update a specific meal in the 
        //TODO: search for any meal in spoonacular, needs a helper method
        // [Authorize]
        // [HttpPut("updateMealPlanWithSpoonacularMeal")]
        // public async Task<IActionResult> UpdateMealPlanWithSpoonacularMeal(string mealId, DayOfWeek day, int mealIndexInDay)
        // {

        //     //TODO: Gets the meal in the meal plan and recplaces it with the choosen meal


        //     //make sure it saves to the user
        //     //get the logged in user 
        //     var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).FirstOrDefaultAsync(us => us.NormalizedEmail
        //     .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

        //     //find the meal in the database that they want to replace with
        //     //the spoonacular id should be empty if its a user defined meal
        //     var mealInDb = _context.Meals.Where(m => string.IsNullOrEmpty(m.SpoonacularMealId.ToString()) == true)
        //     .FirstOrDefaultAsync(userMeals => userMeals.Id.Equals(new Guid(mealId.Trim())));

        //     //replace the user meal with the new meal
        //     //get the meal plan, get the day of the week, get the meal object in the list of meals for that day
        //     loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals[mealIndexInDay] = await mealInDb;

        //     await _context.SaveChangesAsync();

        //     //show the updated meal for that day
        //     return Ok(loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals.ToList());
        // }




        //Mike
        //GET meal plan (DB)


        //Mike
        //DELETE a meal from the meal plan
    }
}