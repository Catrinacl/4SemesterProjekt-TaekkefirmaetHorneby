using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data_Layer.Context
{
    internal class ProjektContext : DbContext
    {
        public ProjektContext()
        {
            bool created = Database.EnsureCreated();
            if (created)
            {
                Debug.WriteLine("Database created");
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO
            //optionsBuilder.UseSqlServer("");
            //optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IProjekt>().HasData(new ProjektContext[] {
                //TODO projekt opsætning
            });
            modelBuilder.Entity<Arbejdsopgave>().HasData(new Arbejdsopgave[] {
                //TODO skal måske ikke ligge her??
            });
        }
        public DbSet<IProjekt> projekts { get; set; }
        public DbSet<Arbejdsopgave> arbejdsopgaves { get; set; }
    }
}
