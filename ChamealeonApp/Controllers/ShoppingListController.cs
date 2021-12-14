using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ChamealeonApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ChamealeonApp.Models.Persistence;
using ChamealeonApp.Models.Authentication;

namespace ChamealeonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {

        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;

         public ShoppingListController(DataContext context, UserManager<User> userManager,
            TokenService tokenService)
        {
            _context = context;
            this._userManager = userManager;
        }

        //mike
        //GET general ingredients (from db)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var loggedInUser = await _userManager.Users
                                        .Include(x => x.CurrentMealPlan)
                                        .ThenInclude(x => x.MealDays)
                                        .ThenInclude(x => x.Meals)
                                        .ThenInclude(x => x.Ingredients)
                                        .FirstOrDefaultAsync(x => x.NormalizedEmail.Equals(User.FindFirstValue(ClaimTypes.Email).ToUpper()));

            var mealDays = loggedInUser.CurrentMealPlan.MealDays;

            var ingredientsList = new List<Ingredient>();

            //for each of the user's meals in the meal plan, go through each day, then each meal in each day and add those ingredients to the shoppoing list
            for(int i = 0; i < mealDays.Count(); i++)
                for(int j = 0; j < mealDays[i].Meals.Count(); j++)
                    ingredientsList.AddRange(mealDays[i].Meals[j].Ingredients);
            
            var groupedIngredients = ingredientsList.GroupBy(x => x.Name);

            return Ok(groupedIngredients);
        }
    }
}