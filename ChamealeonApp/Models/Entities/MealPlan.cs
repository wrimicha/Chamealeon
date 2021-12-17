using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Author: Amir: Meal Plan Class that has a relation to meal days for each day
namespace ChamealeonApp.Models.Entities
{
    public class MealPlan
    {
        //Amir
        public Guid Id { get; set; }

        public List<DaysMeal> MealDays { get; set; }
    }
}