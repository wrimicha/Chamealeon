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
    public class MealPlanController : Controller
    {
        //burhan
        //GET generate weekly meal plan (calls helper API)

        //PUT update a specific meal in the weekly meal plan (needs to get a user defined meal)
        //helper func: search for user made meal within db
        //helper func: for multiple suggested meal from (call helper API)

        //GET meal plan (DB)

        //DELETE a meal from the meal plan
    }
}