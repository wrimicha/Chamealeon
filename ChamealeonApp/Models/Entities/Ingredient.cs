using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.Entities
{
    public class Ingredient
    {
        //Mike
        public Guid Id { get; set; }

        //public int SpoonacularMealId {get; set;} ?
        public string Name { get; set; }

        // public double Cost { get; set; }

        // public List<Meal> Meals {get; set;}

        // public string ImageUrl { get; set; }

        public string UnitOfMeasurement { get; set; }

        public double Amount { get; set; }

        //public NutritionalInformation nutritionalInformation {get; set;}
    }
}