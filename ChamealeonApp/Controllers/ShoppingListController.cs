using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChamealeonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        //mike
        //GET general ingredients (from db)

        // [HttpGet]
        // public async Task<IActionResult> Get([FromQuery] string userId)
        // {
        //     if(userId == null || userId  == "") return BadRequest("No valid id was provided");
            
        //     //get the current meal plan
        //     var userIngredients = _context.User
        //                             .SingleOrDefault(x => x.Id.isEqual(userId))
        //                             .Include(x => x.MealPlan.Meals)
        //                             .Include(x => x.ingredients);

        //     return Ok(userIngredients);
        // }
        


        // //GET full detailed ingredients for 21 meals (dropdown list, shows specific quantity)??

        // [HttpGet]
        // public async Task<IActionResult> Get([FromQuery] string userId)
        // {
        //      var mealsWithIngredients = _context.User
        //                                 .SingleOrDefault(x => x.Id.isEqual(userId))
        //                                 .Includes(x => x.MealPlan.Mea)
        //     return Ok();
        // }
    }
}