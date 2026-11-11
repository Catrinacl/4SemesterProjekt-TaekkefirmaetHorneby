using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model
{
    public class LangtProjekt : IProjekt
    {
        public int ProjektID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Arbejdsopgave> Arbejdsopgaver { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Adress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double ExpectedDurationHours { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double RoofSqrM { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
