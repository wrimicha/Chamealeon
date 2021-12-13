using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.Entities
{
    public class Meal
    {
        //Burhan
        public Guid Id { get; set; }
        public int SpoonacularMealId { get; set; }
        public List<Ingredient> Ingredients { get; set; } //many to many
        //public string MealType { get; set; }
        public double Cost { get; set; }
        public double PrepTime { get; set; }
        public string Instructions { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public NutritionalInformation NutritionInfo { get; set; }

    }
}