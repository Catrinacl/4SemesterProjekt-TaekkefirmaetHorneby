using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Model
{
    public class Arbejdsopgave
    {
        [Key]
        public int ArbejdsopgaveID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;
        public double SvendProductionPerDay { get; set; }
        public double LaerlingProductionPerDay { get; set; }
        public bool RequiresManualTime { get; set; }


        
        // Til Entity Framework
        public Arbejdsopgave() { }
    }
}