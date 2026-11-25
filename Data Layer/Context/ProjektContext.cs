using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using DataAccess.Model;

namespace DataAccess.Context
{
    internal class ProjektContext : DbContext
    {
        private static bool databaseInitialized = false;
        private static readonly object databaseLock = new object();
        public ProjektContext()
        {
            lock (databaseLock)
            {
                if (!databaseInitialized)
                {
                    Database.EnsureDeleted();
                    Database.EnsureCreated();

                    databaseInitialized = true;
                }
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

            modelBuilder.Entity<ProjektArbejdsopgave>()
                .HasOne(p => p.Arbejdsopgave)
                .WithMany()
                .HasForeignKey(p => p.ArbejdsopgaveID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<ProjektArbejdsopgave>()
                .HasOne(p => p.DagsProjekt)
                .WithMany(p => p.Arbejdsopgaver)
                .HasForeignKey(p => p.DagsProjektID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProjektArbejdsopgave>()
                .HasOne(p => p.LangtProjekt)
                .WithMany(p => p.Arbejdsopgaver)
                .HasForeignKey(p => p.LangtProjektID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DagsProjekt>()
                .HasMany(p => p.Medarbejdere)
                .WithMany(m => m.DagsProjekter);

            modelBuilder.Entity<LangtProjekt>()
                .HasMany(p => p.Medarbejdere)
                .WithMany(m => m.LangtProjekter);

            modelBuilder.Entity<DagsProjekt>()
                .HasOne(p => p.Kunde)
                .WithMany(k => k.DagsProjekter)
                .HasForeignKey(p => p.Kundenummer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LangtProjekt>()
                .HasOne(p => p.Kunde)
                .WithMany(k => k.LangtProjekter)
                .HasForeignKey(p => p.Kundenummer)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<LangtProjekt> Langeprojekter { get; set; }
        public DbSet<Arbejdsopgave> Arbejdsopgaver { get; set; }
        public DbSet<DagsProjekt> Dagsprojekter { get; set; }

        public DbSet<Kunde> Kunder { get; set; }

        public DbSet<Medarbejder> Medarbejdere { get; set; }

        public DbSet<ProjektArbejdsopgave> ProjektArbejdsopgaver { get; set; }
    }
}
