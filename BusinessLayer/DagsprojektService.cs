using DataAccess.Model;
using DataAccess.Repositories;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class DagsprojektService
    {
        private readonly ProjektRepository _projektRepository = new ProjektRepository();
        public void OpretNytDagsProjekt(OpretDagsprojektDTO nytProjektFraUI)
        {
            DagsProjekt dagsprojekt = new DagsProjekt
            {

                Adress = nytProjektFraUI.Adress,
                Status = nytProjektFraUI.Status,
                ExpectedDurationHours = nytProjektFraUI.ExpectedDurationHours,
                RoofSqrM = nytProjektFraUI.RoofSqrM,
                Startdate = nytProjektFraUI.Startdate,
                Endate = nytProjektFraUI.Endate

            };

            _projektRepository.GemDagsProjekt(dagsprojekt);
        }
    }
}
