
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DagsProjektRepository
    {
        public List<DagsProjekt> GetAlleDagsProjekter()
        {
            using (var context = new ProjektContext())
            {
                return context.Dagsprojekter
                    .Include(p => p.Arbejdsopgaver)
                        .ThenInclude(pa => pa.Arbejdsopgave)
                    .Include(p => p.Medarbejdere)
                    .ToList();
            }
        }

        public DagsProjekt GetDagsProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                return context.Dagsprojekter
                    .Include(p => p.Arbejdsopgaver)
                        .ThenInclude(pa => pa.Arbejdsopgave)
                    .Include(p => p.Medarbejdere)
                    .FirstOrDefault(p => p.ProjektID == id);
            }
        }

        public void GemDagsProjekt(DagsProjekt dagsprojekt)
        {
            using (var context = new ProjektContext())
            {
                foreach (var projektArbejdsopgave in dagsprojekt.Arbejdsopgaver)
                {
                    projektArbejdsopgave.Arbejdsopgave = null;
                    projektArbejdsopgave.DagsProjekt = null;
                    projektArbejdsopgave.DagsProjektID = null;
                }

                // Find de eksisterende medarbejdere i databasen,
                // så de ikke bliver oprettet som nye medarbejdere.
                var medarbejderIDer = dagsprojekt.Medarbejdere
                    .Select(m => m.ID)
                    .Distinct()
                    .ToList();

                dagsprojekt.Medarbejdere.Clear();

                foreach (var medarbejderID in medarbejderIDer)
                {
                    var eksisterendeMedarbejder = context.Medarbejdere
                        .Find(medarbejderID);

                    if (eksisterendeMedarbejder != null)
                    {
                        dagsprojekt.Medarbejdere.Add(
                            eksisterendeMedarbejder);
                    }
                }

                context.Dagsprojekter.Add(dagsprojekt);
                context.SaveChanges();
            }
        }

        public void OpdaterDagsProjekt(DagsProjekt dagsprojekt)
        {
            using (var context = new ProjektContext())
            {
                var eksisterendeProjekt = context.Dagsprojekter
                    .Include(p => p.Arbejdsopgaver)
                    .Include(p => p.Medarbejdere)
                    .FirstOrDefault(p => p.ProjektID == dagsprojekt.ProjektID);

                if (eksisterendeProjekt == null)
                {
                    return;
                }

                context.Entry(eksisterendeProjekt).CurrentValues
                    .SetValues(dagsprojekt);

                context.ProjektArbejdsopgaver.RemoveRange(
                    eksisterendeProjekt.Arbejdsopgaver);

                eksisterendeProjekt.Medarbejdere.Clear();

                foreach (var medarbejderID in dagsprojekt.Medarbejdere
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

                foreach (var projektArbejdsopgave in dagsprojekt.Arbejdsopgaver)
                {
                    projektArbejdsopgave.ProjektArbejdsopgaveID = 0;
                    projektArbejdsopgave.Arbejdsopgave = null;
                    projektArbejdsopgave.DagsProjekt = null;
                    projektArbejdsopgave.DagsProjektID =
                        eksisterendeProjekt.ProjektID;
                    projektArbejdsopgave.LangtProjekt = null;
                    projektArbejdsopgave.LangtProjektID = null;

                    eksisterendeProjekt.Arbejdsopgaver.Add(
                        projektArbejdsopgave);
                }

                context.SaveChanges();
            }
        }

        public void SletDagsProjekt(int id)
        {
            using (var context = new ProjektContext())
            {
                var dagsprojekt = context.Dagsprojekter
                    .FirstOrDefault(p => p.ProjektID == id);

                if (dagsprojekt != null)
                {
                    context.Dagsprojekter.Remove(dagsprojekt);
                    context.SaveChanges();
                }
            }
        }
    }
}
