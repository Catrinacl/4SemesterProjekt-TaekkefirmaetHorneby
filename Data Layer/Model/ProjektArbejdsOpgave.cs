
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class ProjektArbejdsopgave
    {
        [Key]
        public int ProjektArbejdsopgaveID { get; set; }

        [Required]
        public int ArbejdsopgaveID { get; set; }

        public Arbejdsopgave Arbejdsopgave { get; set; }

        public double Maengde { get; set; }

        public double ExpectedDurationHours { get; set; }

        public double? ManualTimeHours { get; set; }

        public int? DagsProjektID { get; set; }

        public DagsProjekt? DagsProjekt { get; set; }

        public int? LangtProjektID { get; set; }

        public LangtProjekt? LangtProjekt { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Priority { get; set; }
    }
}
