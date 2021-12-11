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
    [ApiController]
    [Route("api/[controller]")]
    public class MealPlanController : Controller
    {
        //burhan

        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        // private readonly SignInManager<User> _signInManager;
        // private readonly TokenService _tokenService;

        public MealPlanController(DataContext context, UserManager<User> userManager
            // SignInManager<User> signInManager,
            // TokenService tokenService
            )
        {
            _context = context;
            this._userManager = userManager;
            // this._signInManager = signInManager;
            // _tokenService = tokenService;
        }

        //make weekly meal plan (calls helper API)
        [Authorize]
        [HttpPost("generateMealPlan")]
        public async Task<IActionResult> GenereateMealPlan([FromBody] MealPlanQueryDTO mealPlanQuery)
        {
            //make sure it saves to the user
            //get the logged in user 
            var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).Include(u => u.PersonalNutritionalInformationGoal).FirstOrDefaultAsync(us => us.NormalizedEmail
            .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //set the query to the users goals


            //TODO: check the list of items to include for any user input errors

            //call helper to make a request to API and save to the database
            var retrievedRootResponse = await SpoonacularAPIHelper.GenerateMealPlanFromSpoonacularAsync(loggedInUser.Diet.Trim(), mealPlanQuery.ItemsToExclude, loggedInUser.PersonalNutritionalInformationGoal.Calories); //TODO: error check if its not an integer

            var convertedMealPlan = await MealPlanResponseHelper.ConvertRootDTOToMealPlanAsync(retrievedRootResponse, _context);

            // _context.MealPlans.Add(convertedMealPlan);
            // await _context.SaveChangesAsync();





            //might need to include nutrition
            // var userInDb = await _context.Users.Include(u => u.CurrentMealPlan).FirstOrDefaultAsync(u => u.Id.Equals(loggedInUser.Id));

            loggedInUser.CurrentMealPlan = convertedMealPlan;
            // await _context.MealPlans.AddAsync(convertedMealPlan); //not sure if needed
            // userInDb.CurrentMealPlan.Id = convertedMealPlan.Id;
            await _context.SaveChangesAsync();

            return Ok(convertedMealPlan);
        }



        // [HttpGet("test")]
        // public async Task<IActionResult> Get()
        // {
        //     //TODO: Implement Realistic Implementation
        //     var result = await SpoonacularAPIHelper.GenerateMealPlanFromSpoonacularAsync(null, new List<string> { "shellfish", "olives", "chicken", "cheese", "" }, 3000);

        //     return Ok(result);
        // }


        //BURHAN
        //PUT update a specific meal in the weekly meal plan FROM USER'S MEALS CREATED IN DB
        //GET generate weekly meal plan (calls helper API)
        [Authorize]
        [HttpPut("updateMealPlanWithUserMeal")]

        public async Task<IActionResult> UpdateMealPlanWithUserMeal(string mealId, DayOfWeek day, int mealIndexInDay)
        {
            //make sure it saves to the user
            //get the logged in user 
            var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).FirstOrDefaultAsync(us => us.NormalizedEmail
            .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //find the meal in the database that they want to replace with
            //the spoonacular id should be empty if its a user defined meal
            var mealInDb = _context.Meals.Where(m => string.IsNullOrEmpty(m.SpoonacularMealId.ToString()) == true)
            .FirstOrDefaultAsync(userMeals => userMeals.Id.Equals(new Guid(mealId.Trim())));

            //replace the user meal with the new meal
            //get the meal plan, get the day of the week, get the meal object in the list of meals for that day
            loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals[mealIndexInDay] = await mealInDb;

            await _context.SaveChangesAsync();

            //show the updated meal for that day
            return Ok(loggedInUser.CurrentMealPlan.MealDays[(int)day].Meals.ToList());
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

            //TODO: Add Error Checking

            //get the user meals order by the day of the week
            var loggedInUser = await _userManager.Users
                                                 .Include(u => u.CurrentMealPlan)
                                                 .ThenInclude(m => m.MealDays.OrderBy(md=>md.Day))
                                                 .ThenInclude(md => md.Meals)
                                                 .FirstOrDefaultAsync(us => us.NormalizedEmail
                                                 .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //get the user's meal plan
            var mealPlan = loggedInUser.CurrentMealPlan;

            return Ok(mealPlan);
        }

        //Mike
        //DELETE a meal from the meal plan
    }
}