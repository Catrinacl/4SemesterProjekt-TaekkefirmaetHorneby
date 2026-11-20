
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Model;

namespace DataAccess.Repositories
{
    public class DagsProjektRepository
    {
     
        public List<DagsProjekt> GetAlleDagsProjekter()
        {
            using (var context = new ProjektContext())
            {
                return context.Dagsprojekter.ToList();
            }
        }


        public DagsProjekt GetDagsProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                return context.Dagsprojekter.Find(id);
            }
        }

        
        public void GemDagsProjekt(DagsProjekt dagsprojekt)
        {
            using (var context = new ProjektContext())
            {
                context.Dagsprojekter.Add(dagsprojekt);
                context.SaveChanges();
            }
        }

        
        public void OpdaterDagsProjekt(DagsProjekt dagsprojekt)
        {
            using (var context = new ProjektContext())
            {
                context.Dagsprojekter.Update(dagsprojekt);
                context.SaveChanges();
            }
        }

        
        public void SletDagsProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                DagsProjekt dagsprojekt =
                    context.Dagsprojekter.Find(id);

                if (dagsprojekt != null)
                {
                    context.Dagsprojekter.Remove(dagsprojekt);
                    context.SaveChanges();
                }
            }
        }
    }
}
