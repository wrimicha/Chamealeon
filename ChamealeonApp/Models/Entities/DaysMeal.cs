using System;
using System.Collections.Generic;

namespace ChamealeonApp.Models.Entities
{

    //Mike
    public class DaysMeal
    {
        public Guid Id { get; set; }
        public DayOfWeek Day { get; set; }
        public List<Meal> Meals { get; set; }
    }
}