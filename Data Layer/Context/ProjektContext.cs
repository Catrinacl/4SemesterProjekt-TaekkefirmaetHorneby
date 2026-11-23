using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DataAccess.Model;

namespace DataAccess.Context
{
    internal class ProjektContext : DbContext
    {

        private static bool _databaseChecked = false;

        public ProjektContext()
        {
            if (!_databaseChecked)
            {
                Database.EnsureCreated();
                _databaseChecked = true;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
            "Server=tcp:horneby-sql-server.database.windows.net,1433;" +
            "Initial Catalog=HornebyDatabase;" +
            "Persist Security Info=False;" +
            "User ID=hornebyadmin;" +
            "Password=123horneby123!;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=60;",
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(15),
                errorNumbersToAdd: null));

            optionsBuilder.LogTo(message => Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Arbejdsopgave>().HasData(new Arbejdsopgave[] {
                //TODO skal laves når vi er sikre på modellerne og relationerne mellem dem.
            });
        }
       
        public DbSet<LangtProjekt> Langeprojekter { get; set; }
        public DbSet<Arbejdsopgave> Arbejdsopgaver { get; set; }
        public DbSet<DagsProjekt> Dagsprojekter { get; set; }

        public DbSet<Kunde> Kunder { get; set; }

        public DbSet<Medarbejder> Medarbejdere { get; set; }
        public DbSet<ArbejdsTeam> ArbejdsTeams { get; set; }
    }
}
