
using System.Collections.Generic;
using System.Linq;
using DataAccess.Model;
using DataAccess.Repositories;
using DTO;

namespace BusinessLogic
{
    public class ArbejdsopgaveService
    {
        private readonly ArbejdsopgaveRepository _repository =
            new ArbejdsopgaveRepository();

        public List<OpretArbejdsopgaveDTO> GetAlleArbejdsopgaver()
        {
            List<Arbejdsopgave> arbejdsopgaver =
                _repository.GetAlleArbejdsopgaver();

            return arbejdsopgaver.Select(a => new OpretArbejdsopgaveDTO
            {
                ArbejdsopgaveID = a.ArbejdsopgaveID,
                Name = a.Name,
                Description = a.Description,
                Unit = a.Unit,
                SvendProductionPerDay = a.SvendProductionPerDay,
                LaerlingProductionPerDay = a.LaerlingProductionPerDay,
                RequiresManualTime = a.RequiresManualTime
            }).ToList();
        }

        public void OpretArbejdsopgave(OpretArbejdsopgaveDTO nyArbejdsopgaveFraUI)
        {
            Arbejdsopgave arbejdsopgave = new Arbejdsopgave
            {
                Name = nyArbejdsopgaveFraUI.Name,
                Description = nyArbejdsopgaveFraUI.Description,
                Unit = nyArbejdsopgaveFraUI.Unit,
                SvendProductionPerDay = nyArbejdsopgaveFraUI.SvendProductionPerDay,
                LaerlingProductionPerDay = nyArbejdsopgaveFraUI.LaerlingProductionPerDay,
                RequiresManualTime = nyArbejdsopgaveFraUI.RequiresManualTime
            };

            _repository.GemArbejdsopgave(arbejdsopgave);
        }

        public void OpdaterArbejdsopgave(OpretArbejdsopgaveDTO opgaveFraUI)
        {
            Arbejdsopgave eksisterendeOpgave =
                _repository.GetArbejdsopgave(opgaveFraUI.ArbejdsopgaveID);

            if (eksisterendeOpgave == null)
            {
                return;
            }

            eksisterendeOpgave.Name = opgaveFraUI.Name;
            eksisterendeOpgave.Description = opgaveFraUI.Description;
            eksisterendeOpgave.Unit = opgaveFraUI.Unit;
            eksisterendeOpgave.SvendProductionPerDay = opgaveFraUI.SvendProductionPerDay;
            eksisterendeOpgave.LaerlingProductionPerDay = opgaveFraUI.LaerlingProductionPerDay;
            eksisterendeOpgave.RequiresManualTime = opgaveFraUI.RequiresManualTime;

            _repository.OpdaterArbejdsopgave(eksisterendeOpgave);
        }

        public void SletArbejdsopgave(int id)
        {
            _repository.SletArbejdsopgave(id);
        }
    }
}
