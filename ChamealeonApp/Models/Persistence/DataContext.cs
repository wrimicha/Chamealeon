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
        public DbSet<NutritionalInformation> NutrionalInformations { get; set; }
        // public DbSet<User> Users { get; set; } //TODO: not need?
        //public DbSet<DaysMeal> DaysMeals { get; set; }

        //Had an error with migrations: https://stackoverflow.com/questions/57745481/unable-to-create-an-object-of-type-mycontext-for-the-different-patterns-suppo
        public DataContext()
        {
        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //database name will be the one that currently is existing, Data.db
            optionsBuilder.UseSqlite("FileName=ChamealeonDb.db");
        }
    }
}