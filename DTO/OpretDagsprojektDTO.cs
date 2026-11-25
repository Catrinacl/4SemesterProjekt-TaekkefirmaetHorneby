
using System;
using System.Collections.Generic;

namespace DTO
{
    public class OpretDagsprojektDTO
    {
        public int ProjektID { get; set; }
        public int KundeID { get; set; }

        public string Adress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }

        public DateTime Startdate { get; set; }
        public DateTime Endate { get; set; }

        public List<int> MedarbejderIds { get; set; } = new();

        public List<ValgtArbejdsopgaveDTO> Arbejdsopgaver { get; set; } = new();
    }
}
