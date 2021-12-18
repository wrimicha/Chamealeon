using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.Entities
{
    public class Ingredient
    {
        //Mike
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string UnitOfMeasurement { get; set; }

        public double Amount { get; set; }
    }
}