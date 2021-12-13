using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChamealeonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        // private readonly SignInManager<User> _signInManager;
        // private readonly TokenService _tokenService;
        public NutritionController(DataContext context, UserManager<User> userManager
            // SignInManager<User> signInManager,
            // TokenService tokenService
            )
        {
            _context = context;
            this._userManager = userManager;
            // this._signInManager = signInManager;
            // _tokenService = tokenService;
        }
        //burhan
        //GET weekly nutrional information (macros) from db for the week
        [Authorize]
        [HttpGet("weeklyInformation")]
        public async Task<IActionResult> GetWeeklyNutritionalInformation()
        {
            //create an object of nutrional information to send to the view
            var totalNutritionalInformation = new NutritionalInformation();

            //create a list of the user's meals
            //get the logged in user 
            var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).ThenInclude(meals => meals.NutritionInfo)
            .FirstOrDefaultAsync(us => us.NormalizedEmail
            .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //in the meal plan, iterate through each day
            //in each day, iterate through each of the 3 meals
            //in each meal, add the macros
            foreach (var day in loggedInUser.CurrentMealPlan.MealDays)
            {
                foreach (var meals in day.Meals)
                {
                    totalNutritionalInformation.Calories += meals.NutritionInfo.Calories;
                    totalNutritionalInformation.Carbs += meals.NutritionInfo.Carbs;
                    totalNutritionalInformation.Fat += meals.NutritionInfo.Fat;
                    totalNutritionalInformation.Protein += meals.NutritionInfo.Protein;
                    totalNutritionalInformation.Sodium += meals.NutritionInfo.Sodium;
                    totalNutritionalInformation.Sugar += meals.NutritionInfo.Sugar;

                }
            }



            //go through the entire nutrional information table
            // var nutritionalInformationForTheWeek = _context.NutritionalInformations;

            // foreach (var nutrition in nutritionalInformationForTheWeek)
            // {
            //     totalNutritionalInformation.Calories += nutrition.Calories;
            //     totalNutritionalInformation.Carbohydrates += nutrition.Carbohydrates;
            //     totalNutritionalInformation.Fat += nutrition.Fat;
            //     totalNutritionalInformation.Protein += nutrition.Protein;
            // }

            //OR
            //might need .include()
            // totalNutritionalInformation.Calories = loggedInUser.CurrentMealPlan.MealDays.Max.Sum(n => n.Calories);
            // totalNutritionalInformation.Carbohydrates = _context.NutritionalInformations.Sum(n => n.Carbohydrates);
            // totalNutritionalInformation.Fat = _context.NutritionalInformations.Sum(n => n.Fat);
            // totalNutritionalInformation.Protein = _context.NutritionalInformations.Sum(n => n.Protein);

            //display entire object to view where each attribute can be accessed
            return Ok(totalNutritionalInformation);
        }

        //GET weekly nutrional information (macros) from db for the day
        [Authorize]
        [HttpGet("dailyInformation")]
        public async Task<IActionResult> GetDailyNutritionalInformation([FromQuery] string day)
        {
            //if user does not specify the day
            if (String.IsNullOrEmpty(day))
            {
                day = DateTime.Today.DayOfWeek.ToString();
            }

            //find the nutritional information for the specific day

            //create a list of the user's meals
            var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).ThenInclude(meals => meals.NutritionInfo)
            .FirstOrDefaultAsync(us => us.NormalizedEmail
            .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            var mealPlanDay = loggedInUser.CurrentMealPlan.MealDays
            .Where(mealDays => mealDays.Day == Enum.Parse<DayOfWeek>(day));
            //var nutritionalInformationForTheDay = _context.MealPlan.Include(m => m.NutrionalInformation).Where(mp => mp.Day.ToLower().Equals(day.ToLower().Trim()));
            var nutritionalInformationForTheDay = new NutritionalInformation
            {

                Calories = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Calories),
                Carbs = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Carbs),
                Protein = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Protein),
                Sodium = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Sodium),
                Sugar = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Sugar),
                Fat = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Fat)

            };
            //display entire object to view where each attribute can be accessed
            return Ok(nutritionalInformationForTheDay);
        }



    }
}