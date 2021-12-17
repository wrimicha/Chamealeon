using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Author: Amir, entity for nutritional information
namespace ChamealeonApp.Models.Entities
{
    public class NutritionalInformation
    {
        //Amir
        public Guid Id { get; set; }
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Sodium { get; set; }
        public double Sugar { get; set; }
    }
}