using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    internal class DagsProjekt : IProjekt
    {
        //TODO skal sættes baseret på alle projekter
        private static int idCount = 0;
        private int projektID;
        private List<Arbejdsopgave> arbejdsopgaver;
        private String adress;
        private String status;
        private double expextedDurationHours;
        private double roofSqrM;
        private DateTime startdate;
        private DateTime endDate;


        public DagsProjekt(String adress, double roofSqrM)
        {
            this.projektID = idCount++;
            this.adress = adress;
            this.status = "Fremtidigt projekt";
            this.expextedDurationHours = 0;
            this.roofSqrM = roofSqrM;
        }

        public int ProjektID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Arbejdsopgave> Arbejdsopgaver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Adress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double ExpectedDurationHours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double RoofSqrM { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Startdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime Endate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}