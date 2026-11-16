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
    }
}
