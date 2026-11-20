
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Model;

namespace DataAccess.Repositories
{
    public class LangtProjektRepository
    {

        public List<LangtProjekt> GetAlleProjekter()
        {
            using (var context = new ProjektContext())
            {
                return context.Langeprojekter.ToList();
            }
        }


        public LangtProjekt GetProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                return context.Langeprojekter.Find(id);
            }
        }


        public void GemProjekt(LangtProjekt nytProjekt)
        {
            using (var context = new ProjektContext())
            {
                context.Langeprojekter.Add(nytProjekt);
                context.SaveChanges();
            }
        }


        public void OpdaterProjekt(LangtProjekt projekt)
        {
            using (var context = new ProjektContext())
            {
                context.Langeprojekter.Update(projekt);
                context.SaveChanges();
            }
        }


        public void SletProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                LangtProjekt projekt =
                    context.Langeprojekter.Find(id);

                if (projekt != null)
                {
                    context.Langeprojekter.Remove(projekt);
                    context.SaveChanges();
                }
            }
        }

    }
}