using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.DTOs
{
    //Author: Burhan
    //DTO used for excluding a list of items when a meal plan is generated
    public class MealPlanQueryDTO
    {
        public List<string> ItemsToExclude { get; set; }

    }
}