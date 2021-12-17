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
    //Burhan: Implemented swaping a meal in the mealplan with a user created meal from the database
    //Mike: 

    //This controller is responsible for swapping a selected meal in the meal plan with a user created one or a Spoonacular one
    [ApiController]
    [Route("api/[controller]")]
    public class SwapMealController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

        public SwapMealController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        //BURHAN
        //display a list of user meals to the user so that they can select one
        [Authorize]
        [HttpGet("displayUserMeals")]
        public async Task<IActionResult> GetMealDetails()
        {
            //ensure all information is displayed
            var loggedInUser = await _userManager.Users.Include(u => u.CurrentMealPlan).ThenInclude(mp => mp.MealDays).ThenInclude(md => md.Meals)
            .ThenInclude(m => m.Ingredients).Include(u => u.UserCreatedMeals).ThenInclude(m => m.NutritionInfo).FirstOrDefaultAsync(us => us.NormalizedEmail
           .Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            //list of user meals
            //Reference: https://stackoverflow.com/questions/3173718/how-to-get-a-random-object-using-linq/3173726
            var rnd = new Random();
            var userMeals = loggedInUser.UserCreatedMeals.OrderBy(m => rnd.Next()).Take(5);



            return Ok(userMeals);
        }


        //MIKE
        [Authorize]
        [HttpGet("displaySpoonacularMeals")]
        public async Task<IActionResult> GetSpoonacularMealDetails(string query)
        {
            try{
                //TODO: accept values for other filter criteria
                var suggestions = await SpoonacularAPIHelper.GetMealSuggestions(query);

                return Ok(suggestions);
            }
            catch (System.Exception)
            {
                return BadRequest(new ErrorDTO { Title = "An error occured in grabbing meal suggestions from Spoonacular API" });
            }
        }
    }
}