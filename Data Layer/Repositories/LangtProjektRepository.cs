
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class LangtProjektRepository
    {
        public List<LangtProjekt> GetAlleProjekter()
        {
            using (var context = new ProjektContext())
            {
                return context.Langeprojekter
                    .Include(p => p.Arbejdsopgaver)
                        .ThenInclude(pa => pa.Arbejdsopgave)
                    .Include(p => p.Medarbejdere)
                    .ToList();
            }
        }

        public LangtProjekt GetProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                return context.Langeprojekter
                    .Include(p => p.Arbejdsopgaver)
                        .ThenInclude(pa => pa.Arbejdsopgave)
                    .Include(p => p.Medarbejdere)
                    .FirstOrDefault(p => p.ProjektID == id);
            }
        }

        public void GemProjekt(LangtProjekt nytProjekt)
        {
            using (var context = new ProjektContext())
            {
                foreach (var projektArbejdsopgave in nytProjekt.Arbejdsopgaver)
                {
                    projektArbejdsopgave.Arbejdsopgave = null;
                    projektArbejdsopgave.DagsProjekt = null;
                    projektArbejdsopgave.DagsProjektID = null;
                    projektArbejdsopgave.LangtProjekt = null;
                    projektArbejdsopgave.LangtProjektID = null;
                }

                var medarbejderIDer = nytProjekt.Medarbejdere
                    .Select(m => m.ID)
                    .Distinct()
                    .ToList();

                nytProjekt.Medarbejdere.Clear();

                foreach (var medarbejderID in medarbejderIDer)
                {
                    var eksisterendeMedarbejder = context.Medarbejdere
                        .Find(medarbejderID);

                    if (eksisterendeMedarbejder != null)
                    {
                        nytProjekt.Medarbejdere.Add(
                            eksisterendeMedarbejder);
                    }
                }

                context.Langeprojekter.Add(nytProjekt);
                context.SaveChanges();
            }
        }

        public void OpdaterProjekt(LangtProjekt projekt)
        {
            using (var context = new ProjektContext())
            {
                var eksisterendeProjekt = context.Langeprojekter
                    .Include(p => p.Arbejdsopgaver)
                    .Include(p => p.Medarbejdere)
                    .FirstOrDefault(p => p.ProjektID == projekt.ProjektID);

                if (eksisterendeProjekt == null)
                {
                    return;
                }

                context.Entry(eksisterendeProjekt).CurrentValues
                    .SetValues(projekt);


                eksisterendeProjekt.Medarbejdere.Clear();

                foreach (var medarbejderID in projekt.Medarbejdere
                    .Select(m => m.ID)
                    .Distinct())
                {
                    var eksisterendeMedarbejder = context.Medarbejdere
                        .Find(medarbejderID);

                    if (eksisterendeMedarbejder != null)
                    {
                        eksisterendeProjekt.Medarbejdere.Add(
                            eksisterendeMedarbejder);
                    }
                }


                context.ProjektArbejdsopgaver.RemoveRange(
                    eksisterendeProjekt.Arbejdsopgaver);


                foreach (var projektArbejdsopgave in projekt.Arbejdsopgaver)
                {
                    projektArbejdsopgave.ProjektArbejdsopgaveID = 0;
                    projektArbejdsopgave.Arbejdsopgave = null;
                    projektArbejdsopgave.DagsProjekt = null;
                    projektArbejdsopgave.DagsProjektID = null;
                    projektArbejdsopgave.LangtProjekt = null;
                    projektArbejdsopgave.LangtProjektID =
                        eksisterendeProjekt.ProjektID;

                    eksisterendeProjekt.Arbejdsopgaver.Add(
                        projektArbejdsopgave);
                }

                context.SaveChanges();
            }
        }

        public void SletProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                var projekt = context.Langeprojekter
                    .FirstOrDefault(p => p.ProjektID == id);

                if (projekt != null)
                {
                    context.Langeprojekter.Remove(projekt);
                    context.SaveChanges();
                }
            }
        }
    }
}
