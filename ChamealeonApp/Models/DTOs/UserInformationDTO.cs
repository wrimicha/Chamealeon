using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.DTOs
{
    public class UserInformationDTO
    {
        public string Id { get; set; }
        public int Age { get; set; }
        public string Diet { get; set; }
        public double Weight { get; set; } //metric or imperial?
        public double Height { get; set; } //metric or imperial?
                                           //nutrtional information
        public double Calories { get; set; }
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Sodium { get; set; }
        public double Sugar { get; set; }
    }
}