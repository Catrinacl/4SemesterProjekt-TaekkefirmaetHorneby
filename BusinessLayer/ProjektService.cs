using DTO;
using DataAccess;

namespace BusinessLayer
{
    public class ProjektService
    {
        public void OpretNytProjekt(OpretProjekt nytProjektFraUI)
        {
            // Her modtager BusinessLayer de data, som MAUI (UI) har sendt,
            // og kan validere eller regne på dem, inden de sendes til databasen.

            // Fx:
            // if (nytProjektFraUI.KvadratmeterAfTag < 0) throw new Exception("...");

            // Send derefter dataene til jeres DataAccess lag (Repository)
            // ProjektRepository.GemProjekt(nytProjektFraUI);
        }
    }
}