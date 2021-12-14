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

        public List<DaysMeal> MealDays { get; set; }
    }
}