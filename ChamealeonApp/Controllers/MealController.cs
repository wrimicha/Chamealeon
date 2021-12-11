using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

    public class MealController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        public MealController(DataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Amir
        //POST add new user meal to db
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostCustomMeal(int day, [FromBody] Meal meal)

        {
            //TODO: this route only creates a meal, does not add it to meal plan.
            //UpdateMealPlanWithUserMeal in MealPlanController does that


            //TODO error check
            var user = await _userManager.Users.Include(x => x.CurrentMealPlan).FirstOrDefaultAsync(x => x.NormalizedEmail.Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));
            var dayMealToAppendTo = user.CurrentMealPlan.MealDays.SingleOrDefault(x => x.Day == (DayOfWeek)day);
            dayMealToAppendTo.Meals.Add(meal);
            await _userManager.UpdateAsync(user);
            return Created("", null);
        }


        //GET full details of a meal (screen, has instructions etc)
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMealDetails(string id)
        {
            return Ok(await _context.Meals.Include(m => m.Ingredients).Include(m => m.NutritionInfo).FirstOrDefaultAsync(x => x.Id == new Guid(id)));
        }
    }
}