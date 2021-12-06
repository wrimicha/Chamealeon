using System;
using System.Collections.Generic;

namespace ChamealeonApp.Models.Entities
{

    //Mike
    public class DaysMeal
    {
        public DayOfWeek Day { get; set; }
        public List<Meal> Meals { get; set; }
    }
}