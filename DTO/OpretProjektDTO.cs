
using System;
using System.Collections.Generic;

namespace DTO
{
    public class OpretProjektDTO
    {
        public OpretProjektDTO(int kundeID, string adresse, string status,
            double forventetvarighedstimer, double kvadratmeteraftaf)
        {
            KundeID = kundeID;
            Adresse = adresse;
            Status = status;
            ForventetVarighedTimer = forventetvarighedstimer;
            KvadratmeterAfTag = kvadratmeteraftaf;
        }

        public OpretProjektDTO()
        {
        }

        public int ProjektID { get; set; }
        public int KundeID { get; set; }
        public string Adresse { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public double ForventetVarighedTimer { get; set; }
        public double KvadratmeterAfTag { get; set; }

        public List<int> MedarbejderIds { get; set; } = new();

        public List<ValgtArbejdsopgaveDTO> Arbejdsopgaver { get; set; } = new();

        public DateTime Startdate { get; set; }
        public DateTime Endate { get; set; }
    }
}
