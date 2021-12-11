using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.DTOs
{
    public class MealPlanQueryDTO
    {
        //properties of meal plan query
        // public string Diet { get; set; }
        // public double Calories { get; set; }
        public List<string> ItemsToExclude { get; set; }

    }
}