using DTO;
using DataAccess.Model;
using DataAccess.Repositories;


namespace BusinessLayer
{
    public class ProjektService
    {

        private readonly ProjektRepository _projektRepository = new ProjektRepository();
        public void OpretNytProjekt(OpretProjekt nytProjektFraUI)
        {
            LangtProjekt langtProjekt = new LangtProjekt
            {
                Adress = nytProjektFraUI.Adress,
                Status = nytProjektFraUI.Status,
                ExpectedDurationHours = nytProjektFraUI.ExpectedDurationHours,
                RoofSqrM = nytProjektFraUI.RoofSqrM
            };
        }
    }
}