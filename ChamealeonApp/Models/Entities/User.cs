using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamealeonApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ChamealeonApp.Models.Entities
{
    //Author: Burhan
    //Model represents a user object in the app
    public class User : IdentityUser
    {
        //no Id because Identity creates it by default
        public string Name { get; set; } //required
        public string Password { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Diet { get; set; }
        public double Weight { get; set; } //metric
        public double Height { get; set; } //metric
        public NutritionalInformation PersonalNutritionalInformationGoal { get; set; }
        public MealPlan CurrentMealPlan { get; set; }
        public List<Meal> UserCreatedMeals { get; set; }
    }
}

