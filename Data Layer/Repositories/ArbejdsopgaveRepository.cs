
using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Model;

namespace DataAccess.Repositories
{
    public class ArbejdsopgaveRepository
    {
      
        public List<Arbejdsopgave> GetAlleArbejdsopgaver()
        {
            using (var context = new ProjektContext())
            {
                return context.Arbejdsopgaver.ToList();
            }
        }

       
        public Arbejdsopgave GetArbejdsopgave(int id)
        {
            using (var context = new ProjektContext())
            {
                return context.Arbejdsopgaver.Find(id);
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

        
        public void OpdaterArbejdsopgave(Arbejdsopgave arbejdsopgave)
        {
            using (var context = new ProjektContext())
            {
                context.Arbejdsopgaver.Update(arbejdsopgave);
                context.SaveChanges();
            }
        }

        
        public void SletArbejdsopgave(int id)
        {
            using (var context = new ProjektContext())
            {
                Arbejdsopgave arbejdsopgave =
                    context.Arbejdsopgaver.Find(id);

                if (arbejdsopgave != null)
                {
                    context.Arbejdsopgaver.Remove(arbejdsopgave);
                    context.SaveChanges();
                }
            }
        }
    }
}
