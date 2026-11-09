using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Model
{
    internal class DagsProjekt : IProjekt
    {
        private String adress;
        private List<IArbejdsopgave> arbejdsopgaver;
        public DagsProjekt(String adress) 
        {
            this.adress = adress;
        }

        public int ProjektID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IArbejdsopgave> Arbejdsopgaver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Adress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
