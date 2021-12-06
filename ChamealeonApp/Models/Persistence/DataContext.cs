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
        public DbSet<Meal> Meals { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //database name will be the one that currently is existing, Data.db
            optionsBuilder.UseSqlite("FileName=ChamealeonDb.db");
        }
    }
}