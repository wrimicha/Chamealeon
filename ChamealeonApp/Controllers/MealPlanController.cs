using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChamealeonApp.Models.Authentication;
using ChamealeonApp.Models.DTOs;
using ChamealeonApp.Models.DTOs.SpoonacularResonseDTOs.GenerateMealPlanDTOs;
using ChamealeonApp.Models.Entities;
using ChamealeonApp.Models.Helpers;
using ChamealeonApp.Models.Persistence;
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
            //TODO: check the list of items to include for any user input errors

            //call helper to make a request to API and save to the database
            var retrievedRootResponse = await SpoonacularAPIHelper.GenerateMealPlanFromSpoonacularAsync(mealPlanQuery.Diet.Trim(), mealPlanQuery.ItemsToExclude, mealPlanQuery.Calories); //TODO: error check if its not an integer

            var convertedMealPlan = MealPlanResponseHelper.ConvertRootDTOToMealPlan(retrievedRootResponse);

            _context.MealPlans.Add(convertedMealPlan);
            await _context.SaveChangesAsync();



            //make sure it saves to the user
            var loggedInUser = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            //might need to include nutrition
            var userInDb = await _context.Users.Include(u => u.CurrentMealPlan).FirstOrDefaultAsync(u => u.Id.Equals(loggedInUser.Id));

            //TODO: CALL AMIR
            //save the meal plan to the logged in user
            //NOT OK?
            userInDb.CurrentMealPlan = convertedMealPlan;
            userInDb.CurrentMealPlan.Id = convertedMealPlan.Id;
            await _context.SaveChangesAsync();

            return Ok(convertedMealPlan);
        }



        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            //TODO: Implement Realistic Implementation
            await SpoonacularAPIHelper.GenerateMealPlanFromSpoonacularAsync(null, new List<string>(), 3000);

            return Ok();
        }



        //BURHAN
        //PUT update a specific meal in the weekly meal plan FROM USER'S MEALS CREATED IN DB

        //Mike
        //PUT update a specific meal in the weekly meal plan FROM SPOONACULAR REQUEST


        //Mike
        //GET meal plan (DB)


        //Mike
        //DELETE a meal from the meal plan
    }
}