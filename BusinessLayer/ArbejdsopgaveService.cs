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
        public void OpretNyArbejdsopgave(OpretArbejdsopgaveDTO nytProjektFraUI)
        {
            Arbejdsopgave arbejdsopgave = new Arbejdsopgave
            {

                Name = nytProjektFraUI.name,
                Description = nytProjektFraUI.description,
                Status = nytProjektFraUI.status,
                Priority = nytProjektFraUI.priority,
                StartDate = nytProjektFraUI.startDate,
                EndDate = nytProjektFraUI.endDate
            };

            _projektRepository.GemArbejdsopgave(arbejdsopgave);
            
        }
    }
}
