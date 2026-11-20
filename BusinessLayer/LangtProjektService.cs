using System.Collections.Generic;
using System.Linq;
using DTO;
using DataAccess.Model;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class LangtProjektService
    {
        private readonly LangtProjektRepository _repository =
            new LangtProjektRepository();


        public List<OpretProjektDTO> GetAlleProjekter()
        {
            List<LangtProjekt> projekter =
                _repository.GetAlleProjekter();

            return projekter.Select(p => new OpretProjektDTO
            {
                Adresse = p.Adress,
                Status = p.Status,
                ForventetVarighedTimer = p.ExpectedDurationHours,
                KvadratmeterAfTag = p.RoofSqrM
            }).ToList();
        }

 
        public void OpretNytProjekt(OpretProjektDTO nytProjektFraUI)
        {
            LangtProjekt langtProjekt = new LangtProjekt
            {
                Adress = nytProjektFraUI.Adresse,
                Status = nytProjektFraUI.Status,
                ExpectedDurationHours =
                    nytProjektFraUI.ForventetVarighedTimer,
                RoofSqrM = nytProjektFraUI.KvadratmeterAfTag
            };

            _repository.GemProjekt(langtProjekt);
        }


        public void OpdaterProjekt(OpretProjektDTO projektFraUI)
        {
            LangtProjekt eksisterendeProjekt =
                _repository.GetProjekt(projektFraUI.ProjektID);

            if (eksisterendeProjekt == null)
            {
                return;
            }

            eksisterendeProjekt.Adress = projektFraUI.Adresse;
            eksisterendeProjekt.Status = projektFraUI.Status;
            eksisterendeProjekt.ExpectedDurationHours =
                projektFraUI.ForventetVarighedTimer;
            eksisterendeProjekt.RoofSqrM =
                projektFraUI.KvadratmeterAfTag;

            _repository.OpdaterProjekt(eksisterendeProjekt);
        }

     
        public void SletProjekt(int id)
        {
            _repository.SletProjekt(id);
        }
    }
}