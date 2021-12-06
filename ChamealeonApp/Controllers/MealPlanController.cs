using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.DTOs;
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
        [HttpGet("getMealPlan")]
        public async Task<IActionResult> GetMealPlan([FromBody] MealPlanQueryDTO mealPlanQuery)
        {
            //call helper to make a request to API and save to the database

            //make sure it saves to the user
            var loggedInUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            //makeSpoonacularMealPlanRequest(loggedInUser.Id);

            //the database will now have a full meal plan saved
            var fullMealPlan = _context.MealPlans;
            return Ok(fullMealPlan);
        }

        //PUT update a specific meal in the weekly meal plan (needs to get a user defined meal)
        //helper func: search for user made meal within db
        //helper func: for multiple suggested meal from (call helper API)

        //GET meal plan (DB)

        //DELETE a meal from the meal plan
    }
}