using DataAccess.Model;
using DataAccess.Repositories;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ArbejdsopgaveService
    {
        /*
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
         */

        private readonly ProjektRepository _projektRepository = new ProjektRepository();
        public void OpretNyArbejdsopgave(OpretArbejdsopgaveDTO nyArbejdsopgaveFraUI)
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

            _projektRepository.GemArbejdsopgave(arbejdsopgave);
            
        }
    }
}
