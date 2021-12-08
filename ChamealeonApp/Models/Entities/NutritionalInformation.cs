using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.Entities
{
    public class NutritionalInformation
    {
        //Amir
        public Guid Id { get; set; }
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; } //TODO: fix naming
        public double Carbs { get; set; }
        public double Sodium { get; set; }
        public double Sugar { get; set; }
    }
}