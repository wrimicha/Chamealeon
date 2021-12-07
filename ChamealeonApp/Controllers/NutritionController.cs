using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChamealeonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenService _tokenService;
        public NutritionController(DataContext context, UserManager<User> userManager,
            SignInManager<User> signInManager,
            TokenService tokenService)
        {
            _context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            _tokenService = tokenService;
        }
        //burhan
        //GET weekly nutrional information (macros) from db for the week

        /*
         [HttpGet("weeklyInformation")]
        public async Task<IActionResult> GetWeeklyNutritionalInformation()
        {
            //create an object of nutrional information to send to the view
            var totalNutritionalInformation = new NutritionalInformation();

            //create a list of the user's meals
            //TODO: does it need .Include?
            var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));


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
            totalNutritionalInformation.Calories = currentUser.CurrentMealPlan.NutritionalInformations.Sum(n => n.Calories);
            totalNutritionalInformation.Carbohydrates = _context.NutritionalInformations.Sum(n => n.Carbohydrates);
            totalNutritionalInformation.Fat = _context.NutritionalInformations.Sum(n => n.Fat);
            totalNutritionalInformation.Protein = _context.NutritionalInformations.Sum(n => n.Protein);

            //display entire object to view where each attribute can be accessed
            return Ok(totalNutritionalInformation);
        }

        //GET weekly nutrional information (macros) from db for the day
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
            //TODO: does it need .Include?
            var currentUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var nutritionalInformationForTheDay = currentUser.CurrentMealPlan.Include(m => m.NutrionalInformation).Where(mp => mp.Day.ToLower().Equals(day.ToLower().Trim()));
            //var nutritionalInformationForTheDay = _context.MealPlan.Include(m => m.NutrionalInformation).Where(mp => mp.Day.ToLower().Equals(day.ToLower().Trim()));

            //display entire object to view where each attribute can be accessed
            return Ok(totalNutritionalInformation);
            return Ok();
        }
        */


    }
}