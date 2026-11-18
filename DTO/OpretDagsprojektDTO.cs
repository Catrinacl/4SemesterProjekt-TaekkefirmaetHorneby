using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OpretDagsprojektDTO
    {
       
        public string Adress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public double ExpectedDurationHours { get; set; }
        public double RoofSqrM { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Endate { get; set; }
    }
}
