using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.Entities
{
    public class MealPlan
    {
        //Amir
        public Guid Id { get; set; }

        //public User User { get; set; }
        // TODO: figure out how to get multiple days of week with multiple meals

        public List<DaysMeal> MealDays { get; set; }
    }
}