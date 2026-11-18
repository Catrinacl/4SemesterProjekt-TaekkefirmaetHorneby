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

            //Info skal udskiftes med brugerens egne oplysninger, for at kunne køre programmet.
            //Tænker ikke det er sådan vi skal gøre det i sidste ende, men for nu er det en hurtig løsning.
            optionsBuilder.UseSqlServer(
                "Data Source=SimPC\\SQLEXPRESS;" +
                "Initial Catalog=ProjektDatabase;" +
                "User ID=DIT_BRUGERNAVN;" +
                "Password=DIT_PASSWORD;" +
                "TrustServerCertificate=True");

            optionsBuilder.LogTo(message =>
                Debug.WriteLine(message));
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
