using System;
using System.Collections.Generic;


namespace DataAccess.Model
{
    public interface IProjekt
    {
        public int ProjektID { get; set; }
        public List<ProjektArbejdsopgave> Arbejdsopgaver { get; set; }

        public List<Medarbejder> Medarbejdere { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }
        public int Kundenummer { get; set; }
        public Kunde Kunde { get; set; }

    }
}
