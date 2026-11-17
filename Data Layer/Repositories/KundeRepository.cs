using DataAccess.Context;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class KundeRepository
    {
        public List<Kunde> GetAlleKunder()
        {
            using (var context = new ProjektContext())
            {
                return context.Kunder.ToList();
            }

        }


        public void OpretKunde(Kunde kunde)
        {
            using (var context = new ProjektContext())
            {
                context.Kunder.Add(kunde);
                context.SaveChanges();
            }
        }


        public void OpdaterKunde(Kunde kunde)
        {
            using (var context = new ProjektContext())
            {
                context.Kunder.Update(kunde);
                context.SaveChanges();
            }
        }


        public void SletKunde(int kundenummer)
        {
            using (var context = new ProjektContext())
            {
                Kunde? kunde = context.Kunder.Find(kundenummer);

                if (kunde != null)
                {
                    context.Kunder.Remove(kunde);
                    context.SaveChanges();
                }
            }
        }


    }

}
