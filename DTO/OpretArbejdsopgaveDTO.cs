using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OpretArbejdsopgaveDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
