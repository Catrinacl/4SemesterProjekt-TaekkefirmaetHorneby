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

    }
            
}
