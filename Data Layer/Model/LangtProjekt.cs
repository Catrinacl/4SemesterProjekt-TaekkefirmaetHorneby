using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Model
{

    public class LangtProjekt : IProjekt
    {
        [Key]
        public int ProjektID { get; set; }
        public List<Arbejdsopgave> Arbejdsopgaver { get; set; } = new();
        public string Adress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }
    }
}
