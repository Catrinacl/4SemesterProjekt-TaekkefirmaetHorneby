using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class Arbejdsopgave
    {
        public int ArbejdsopgaveID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        
        // Til Entity Framework
        public Arbejdsopgave() { }
    }
}