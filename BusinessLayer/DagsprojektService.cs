
using System.Collections.Generic;
using System.Linq;
using DataAccess.Model;
using DataAccess.Repositories;
using DTO;

namespace BusinessLogic
{
    public class DagsprojektService
    {
        private readonly DagsProjektRepository _repository =
            new DagsProjektRepository();

        
        public List<OpretDagsprojektDTO> GetAlleDagsProjekter()
        {
            List<DagsProjekt> dagsprojekter =
                _repository.GetAlleDagsProjekter();

            return dagsprojekter.Select(p => new OpretDagsprojektDTO
            {
                ProjektID = p.ProjektID,
                Adress = p.Adress,
                Status = p.Status,
                ExpectedDurationHours = p.ExpectedDurationHours,
                RoofSqrM = p.RoofSqrM,
                Startdate = p.Startdate,
                Endate = p.Endate
            }).ToList();
        }

        
        public void OpretNytDagsProjekt(OpretDagsprojektDTO nytProjektFraUI)
        {
            DagsProjekt dagsprojekt = new DagsProjekt
            {
                Adress = nytProjektFraUI.Adress,
                Status = nytProjektFraUI.Status,
                ExpectedDurationHours =
                    nytProjektFraUI.ExpectedDurationHours,
                RoofSqrM = nytProjektFraUI.RoofSqrM,
                Startdate = nytProjektFraUI.Startdate,
                Endate = nytProjektFraUI.Endate
            };

            _repository.GemDagsProjekt(dagsprojekt);
        }

        
        public void OpdaterDagsProjekt(OpretDagsprojektDTO projektFraUI)
        {
            DagsProjekt eksisterendeProjekt =
                _repository.GetDagsProjekt(projektFraUI.ProjektID);

            if (eksisterendeProjekt == null)
            {
                return;
            }

            eksisterendeProjekt.Adress = projektFraUI.Adress;
            eksisterendeProjekt.Status = projektFraUI.Status;
            eksisterendeProjekt.ExpectedDurationHours =
                projektFraUI.ExpectedDurationHours;
            eksisterendeProjekt.RoofSqrM = projektFraUI.RoofSqrM;
            eksisterendeProjekt.Startdate = projektFraUI.Startdate;
            eksisterendeProjekt.Endate = projektFraUI.Endate;

            _repository.OpdaterDagsProjekt(eksisterendeProjekt);
        }

        
        public void SletDagsProjekt(int id)
        {
            _repository.SletDagsProjekt(id);
        }
    }
}
