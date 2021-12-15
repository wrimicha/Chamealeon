using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.DTOs
{
    //Author: Burhan
    public class UserInformationDTO
    {
        public string Id { get; set; }
        public int Age { get; set; }
        public string Diet { get; set; }
        public double Weight { get; set; } //metric
        public double Height { get; set; } //metric
        public double Calories { get; set; }
    }
}