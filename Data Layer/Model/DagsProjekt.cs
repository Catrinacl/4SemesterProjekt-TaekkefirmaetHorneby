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
        public string Adress { get => adress; set => this.adress = value; }
    }
}
