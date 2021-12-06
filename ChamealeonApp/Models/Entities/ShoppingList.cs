using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.Entities
{
    public class ShoppingList
    {
        //mike

        public List<Ingredient> Ingredients { get; set; }

        public double EstimatedTotalCost { get; set; }

        public List<Meal> Meals { get; set; }

    }
}