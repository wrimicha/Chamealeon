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
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;

        public MealPlanController(DataContext context, UserManager<User> userManager,
            SignInManager<User> signInManager,
            TokenService tokenService)
        {
            _context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            _tokenService = tokenService;
        }

        //GET generate weekly meal plan (calls helper API)
        [Authorize]
        [HttpPost("getMealPlan")]
        public async Task<IActionResult> GetMealPlan([FromBody] MealPlanQueryDTO mealPlanQuery)
        {
            //call helper to make a request to API and save to the database

            //make sure it saves to the user
            //get the logged in user 
            var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).FirstOrDefaultAsync(us => us.NormalizedEmail
            .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //might need to include nutrition
            // var userInDb = await _context.Users.Include(u => u.CurrentMealPlan).FirstOrDefaultAsync(u => u.Id.Equals(loggedInUser.Id));

            loggedInUser.CurrentMealPlan = convertedMealPlan;
            // await _context.MealPlans.AddAsync(convertedMealPlan); //not sure if needed
            // userInDb.CurrentMealPlan.Id = convertedMealPlan.Id;
            await _context.SaveChangesAsync();

            return Ok(convertedMealPlan);
        }



        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            //TODO: Implement Realistic Implementation
            var result = await SpoonacularAPIHelper.GenerateMealPlanFromSpoonacularAsync(null, new List<string>{"shellfish", "olives", "chicken", "cheese", ""}, 3000);
    
            return Ok(result);
        }


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

        //Mike
        //PUT update a specific meal in the weekly meal plan FROM SPOONACULAR REQUEST


        //Mike
        //GET meal plan (DB)


        //Mike
        //DELETE a meal from the meal plan
    }
}