using DTO;
using DataAccess.Model;
using DataAccess.Repositories;


namespace BusinessLayer
{
    public class ProjektService
    {

        private readonly ProjektRepository _projektRepository = new ProjektRepository();
        public void OpretNytProjekt(OpretProjektDTO nytProjektFraUI)
        {
            LangtProjekt langtProjekt = new LangtProjekt
            {

                Adress = nytProjektFraUI.Adresse,
                Status = nytProjektFraUI.Status,
                ExpectedDurationHours = nytProjektFraUI.ForventetVarighedTimer,
                RoofSqrM = nytProjektFraUI.KvadratmeterAfTag

            };

            _projektRepository.GemProjekt(langtProjekt);
        }
    }
}