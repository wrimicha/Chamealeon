using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ChamealeonApp.Models.Entities;

namespace ChamealeonApp.Models.Persistence
{
    public class DataContext : IdentityDbContext<User>
    {
        //Burhan
        //navigation properties

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
        public DbSet<NutrionalInformation> NutrionalInformations { get; set; }
        // public DbSet<User> Users { get; set; } //TODO: not need?
        public DbSet<DaysMeal> Days { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //database name will be the one that currently is existing, Data.db
            optionsBuilder.UseSqlite("FileName=ChamealeonDb.db");
        }
    }
}