using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OpretArbejdsopgaveDTO
    {
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int priority { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }


    }
}
