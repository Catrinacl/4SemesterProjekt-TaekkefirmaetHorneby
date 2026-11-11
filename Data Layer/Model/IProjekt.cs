using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public interface IProjekt
    {
        public int ProjektID { get; set; }
        // TODO: kunde link i senere iteration
        public List<Arbejdsopgave> Arbejdsopgaver { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }

    }
}
