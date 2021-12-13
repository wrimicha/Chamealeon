using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamealeonApp.Models.Entities;

namespace API.Models.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; } //required
                                         // public string Email { get; set; } //required
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Diet { get; set; }
        public double Weight { get; set; } //metric or imperial?
        public double Height { get; set; } //metric or imperial?
        public NutritionalInformation PersonalNutritionalInformationGoal { get; set; } //personal goals, only set calories for the week 
    }
}