using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{

    public class LangtProjekt : IProjekt
    {
        [Key]
        public int ProjektID { get; set; }
        public int Kundenummer { get; set; }
        public Kunde Kunde { get; set; } = null!;
        public List<ProjektArbejdsopgave> Arbejdsopgaver { get; set; } = new();

        public List<Medarbejder> Medarbejdere { get; set; } = new();
        public string Adress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }

        public DateTime Startdate { get; set; }
        public DateTime Endate { get; set; }
    }
}
