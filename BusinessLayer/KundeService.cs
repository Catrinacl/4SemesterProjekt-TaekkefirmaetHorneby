using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DataAccess.Model;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class KundeService
    {
        private readonly KundeRepository _repository = new();

        public List<KundeDTO> GetAlleKunder()
        {
            List<Kunde> kunderFraDb = _repository.GetAlleKunder();

            return kunderFraDb.Select(k => new KundeDTO
            {
                Kundenummer = k.Kundenummer,
                Navn = k.Navn,
                Adresse = k.Adresse,
                Telefonnr = k.Telefonnr,
                Mail = k.Mail
            }).ToList();
        }

        
        public void OpretKunde(KundeDTO kundeDTO)
        {
            Kunde kunde = new Kunde
            {
                Kundenummer = kundeDTO.Kundenummer,
                Navn = kundeDTO.Navn,
                Adresse = kundeDTO.Adresse,
                Telefonnr = kundeDTO.Telefonnr,
                Mail = kundeDTO.Mail
            };

            _repository.OpretKunde(kunde);
        }


        public void OpdaterKunde(KundeDTO kundeDTO)
        {
            Kunde kunde = new Kunde
            {
                Kundenummer = kundeDTO.Kundenummer,
                Navn = kundeDTO.Navn,
                Adresse = kundeDTO.Adresse,
                Telefonnr = kundeDTO.Telefonnr,
                Mail = kundeDTO.Mail
            };

            _repository.OpdaterKunde(kunde);
        }


        public void SletKunde(int kundenummer)
        {
            _repository.SletKunde(kundenummer);
        }
    }
}
