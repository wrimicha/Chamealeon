using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.DTOs;
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
    //Author: Burhan
    //This controller is responsible for getting the nutritional information of the meal plan for the week and a day
    public class NutritionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public NutritionController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        //Author: Burhan
        //GET weekly nutrional information (macros) from for the week
        [Authorize]
        [HttpGet("weeklyInformation")]
        public async Task<IActionResult> GetWeeklyNutritionalInformation()
        {
            try
            {
                //create an object of nutrional information to send to the view
                var totalNutritionalInformation = new NutritionalInformation();

                //get the logged in user 
                var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).ThenInclude(meals => meals.NutritionInfo)
                .FirstOrDefaultAsync(us => us.NormalizedEmail
                .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //in the meal plan, iterate through each day
                foreach (var day in loggedInUser.CurrentMealPlan.MealDays)
                {
                    //in each day, iterate through each of the 3 meals
                    foreach (var meals in day.Meals)
                    {
                        //in each meal, add the nutritional information to our response object
                        totalNutritionalInformation.Calories += meals.NutritionInfo.Calories;
                        totalNutritionalInformation.Carbs += meals.NutritionInfo.Carbs;
                        totalNutritionalInformation.Fat += meals.NutritionInfo.Fat;
                        totalNutritionalInformation.Protein += meals.NutritionInfo.Protein;
                        totalNutritionalInformation.Sodium += meals.NutritionInfo.Sodium;
                        totalNutritionalInformation.Sugar += meals.NutritionInfo.Sugar;
                    }
                }
                return Ok(totalNutritionalInformation);
            }
            catch (System.Exception)
            {
                return BadRequest(new ErrorDTO { Title = "An error has occured calculaitng the nutritional information for the week." });
            }

        }

        //Author: Burhan
        //GET weekly nutrional information (macros) from db for a given day
        [Authorize]
        [HttpGet("dailyInformation")]
        public async Task<IActionResult> GetDailyNutritionalInformation([FromQuery] string day)
        {
            try
            {
                //if the user does not specify the day, get the day of today
                if (String.IsNullOrEmpty(day))
                {
                    day = DateTime.Today.DayOfWeek.ToString();
                }

                //get logged in user
                var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(m => m.MealDays).ThenInclude(md => md.Meals).ThenInclude(meals => meals.NutritionInfo)
                .FirstOrDefaultAsync(us => us.NormalizedEmail
                .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

                //get the meal plan for the day the user wants to know about
                var mealPlanDay = loggedInUser.CurrentMealPlan.MealDays
                .Where(mealDays => mealDays.Day == Enum.Parse<DayOfWeek>(day));

                //response object
                var nutritionalInformationForTheDay = new NutritionalInformation
                {

                    Calories = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Calories),
                    Carbs = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Carbs),
                    Protein = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Protein),
                    Sodium = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Sodium),
                    Sugar = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Sugar),
                    Fat = mealPlanDay.FirstOrDefault().Meals.Sum(meal => meal.NutritionInfo.Fat)

                };

                return Ok(nutritionalInformationForTheDay);
            }
            catch (System.Exception)
            {
                return BadRequest(new ErrorDTO { Title = "An error has occured calculaitng the nutritional information for the given day." });
            }

        }
    }
}