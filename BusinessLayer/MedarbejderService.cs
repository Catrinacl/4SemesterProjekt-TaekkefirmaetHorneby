using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DataAccess.Model;

namespace BusinessLogic
{
    public class MedarbejderService
    {
        private readonly MedarbejderRepository _repository = new();

        public List<MedarbejderDTO> GetAlleMedarbejdere()
        {
            List<Medarbejder> medarbejdereFraDb = _repository.GetAlleMedarbejdere();

            return medarbejdereFraDb.Select(m => new MedarbejderDTO
            {
                ID = m.ID,
                Navn = m.Navn,
                Admin = m.Admin,
                Telefonnr = m.Telefonnr,
                Mail = m.Mail,
                Lærling = m.Lærling
            }).ToList();
        }


        public void OpretMedarbejder(MedarbejderDTO medarbejderDTO)
        {
            Medarbejder medarbejder = new Medarbejder
            {
                ID = medarbejderDTO.ID,
                Navn = medarbejderDTO.Navn,
                Admin = medarbejderDTO.Admin,
                Telefonnr = medarbejderDTO.Telefonnr,
                Mail = medarbejderDTO.Mail,
                Lærling = medarbejderDTO.Lærling
            };

            _repository.OpretMedarbejder(medarbejder);
        }


        public void OpdaterMedarbejder(MedarbejderDTO medarbejderDTO)
        {
            Medarbejder medarbejder = new Medarbejder
            {
                ID = medarbejderDTO.ID,
                Navn = medarbejderDTO.Navn,
                Admin = medarbejderDTO.Admin,
                Telefonnr = medarbejderDTO.Telefonnr,
                Mail = medarbejderDTO.Mail,
                Lærling = medarbejderDTO.Lærling
            };

            _repository.OpdaterMedarbejder(medarbejder);
        }


        public void SletMedarbejder(int id)
        {
            _repository.SletMedarbejder(id);
        }
    }
}
