using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Context;
using DataAccess.Model;

namespace DataAccess.Repositories
{
    public class MedarbejderRepository
    {
        public List<Medarbejder> GetAlleMedarbejdere()
        {
            using (var context = new ProjektContext())
            {
                return context.Medarbejdere.ToList();
            }

        }

    }
}
