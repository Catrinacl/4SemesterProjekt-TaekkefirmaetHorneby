using DataAccess.Context;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repositories
{
    public class ProjektRepository
    {
        public IProjekt GetProjekt(int id)
        {
            // TODO: Tilføj den rigtige databaselogik her senere
            return null;
        }

        public void GemProjekt(LangtProjekt nytProjekt)
        {
            using (var context = new ProjektContext())
            {
                context.Langeprojekter.Add(nytProjekt);
                context.SaveChanges();
            }
        }

        public void GemDagsProjekt(DagsProjekt dagsProjekt)
        {
            using (var context = new ProjektContext())
            {
                context.Dagsprojekter.Add(dagsProjekt);
                context.SaveChanges();
            }
        }

        public void GemArbejdsopgave(Arbejdsopgave arbejdsopgave)
        {
            using (var context = new ProjektContext())
            {
                context.Arbejdsopgaver.Add(arbejdsopgave);
                context.SaveChanges();
            }
        }
    }
}
