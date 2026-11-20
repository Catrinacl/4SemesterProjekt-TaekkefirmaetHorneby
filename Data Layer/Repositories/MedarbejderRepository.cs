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
            using var context = new ProjektContext();
            return context.Medarbejdere.ToList();

        }


        public void OpretMedarbejder(Medarbejder medarbejder)
        {
            using var context = new ProjektContext();
            context.Medarbejdere.Add(medarbejder);
            context.SaveChanges();
        }


        public void OpdaterMedarbejder(Medarbejder medarbejder)
        {
            using (var context = new ProjektContext())
            {
                context.Medarbejdere.Update(medarbejder);
                context.SaveChanges();
            }
        }


        public void SletMedarbejder(int id)
        {
            using (var context = new ProjektContext())
            {
                Medarbejder? medarbejder = context.Medarbejdere.Find(id);

                if (medarbejder != null)
                {
                    context.Medarbejdere.Remove(medarbejder);
                    context.SaveChanges();
                }
            }
        }

    }
}
