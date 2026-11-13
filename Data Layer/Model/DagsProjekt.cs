using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class DagsProjekt : IProjekt
    {
        public int ProjektID { get; set; }
        public List<Arbejdsopgave> Arbejdsopgaver { get; set; } = new();
        public string Adress { get; set; } = string.Empty;
        public string Status { get; set; } = "Fremtidigt projekt";
        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Endate { get; set; }

        
        //Til Entity Framework
        public DagsProjekt() { }

        public DagsProjekt(string adress, double roofSqrM)
        {
            Adress = adress;
            RoofSqrM = roofSqrM;
            Status = "Fremtidigt projekt";
        }
    }
}