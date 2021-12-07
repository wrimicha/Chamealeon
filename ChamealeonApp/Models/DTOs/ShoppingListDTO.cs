using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamealeonApp.Models.Entities;

namespace ChamealeonApp.Models.DTOs
{
    public class ShoppingListDTO
    {
        //mike

        public List<Ingredient> Ingredients { get; set; }

        public double EstimatedTotalCost { get; set; }

        public List<Meal> Meals { get; set; }
    }
}