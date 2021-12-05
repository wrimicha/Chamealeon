using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models
{
    public class Ingredient
    {
        //Mike
        public Guid Id {get; set;}

        //public int SpoonacularMealId {get; set;} ?
        public string Name { get; set; }

        public double Cost { get; set; }

        public List<Meal> Meals {get; set;}

        //public NutritionalInformation nutritionalInformation {get; set;}
    }
}