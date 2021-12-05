using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChamealeonApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChamealeonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : Controller
    {
        //Amir
        //POST add new user meal to db
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User modelUser, Meal modelMeal)
        {
            //TODO: Implement Realistic Implementation
            await Task.Yield();
            return Created("", null);
        }

        //PUT possibly
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User modelUser)
        {
            //TODO: Implement Realistic Implementation
            await Task.Yield();
            return Ok();
        }

        //DELETE delete user meal?
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            //TODO: Implement Realistic Implementation
            return Ok();
        }

        //GET full details of a meal (screen, has instructions etc)
        [HttpGet("{id}")]
        public async Task<IActionResult> Get()
        {
            //TODO: Implement Realistic Implementation
            await Task.Yield();
            return Ok();
        }
    }
}