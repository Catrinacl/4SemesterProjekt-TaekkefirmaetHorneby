using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //DTO - WRITE
    public class OpretProjekt
    {
        public int KundeID { get; set; }
        public string Adresse { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int ForventetVarighedTimer { get; set; }
        public double KvadratmeterAfTag { get; set; }

        public List<int> MedarbejderIds { get; set; } = new();
    }
}
