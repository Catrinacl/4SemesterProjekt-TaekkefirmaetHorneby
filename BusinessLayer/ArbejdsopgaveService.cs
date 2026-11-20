
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
                Status = a.Status,
                Priority = a.Priority,
                StartDate = a.StartDate,
                EndDate = a.EndDate
            }).ToList();
        }

        
        public void OpretArbejdsopgave(OpretArbejdsopgaveDTO nyArbejdsopgaveFraUI)
        {
            Arbejdsopgave arbejdsopgave = new Arbejdsopgave
            {
                Name = nyArbejdsopgaveFraUI.Name,
                Description = nyArbejdsopgaveFraUI.Description,
                Status = nyArbejdsopgaveFraUI.Status,
                Priority = nyArbejdsopgaveFraUI.Priority,
                StartDate = nyArbejdsopgaveFraUI.StartDate,
                EndDate = nyArbejdsopgaveFraUI.EndDate
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
            eksisterendeOpgave.Status = opgaveFraUI.Status;
            eksisterendeOpgave.Priority = opgaveFraUI.Priority;
            eksisterendeOpgave.StartDate = opgaveFraUI.StartDate;
            eksisterendeOpgave.EndDate = opgaveFraUI.EndDate;

            _repository.OpdaterArbejdsopgave(eksisterendeOpgave);
        }

    
        public void SletArbejdsopgave(int id)
        {
            _repository.SletArbejdsopgave(id);
        }
    }
}
