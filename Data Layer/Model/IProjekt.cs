using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Model
{
    internal interface IProjekt
    {
        public int ProjektID { get; set; }
        // TODO: kunde link i senere iteration
        public List<IArbejdsopgave> Arbejdsopgaver { get; set; }
        public string Adress { get; set; }
        public string Status { get; set; }
        
        
        

    }
}
