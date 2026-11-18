using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Mappers
{
    internal class ProjektMapper
    {
        public static DTO.MedarbejderDTO Map(Medarbejder medarbejder)
        {
            return new DTO.MedarbejderDTO (medarbejder.ID, medarbejder.Navn, medarbejder.Admin,
                medarbejder.Telefonnr, medarbejder.Mail, medarbejder.Lærling);
        }
        public static Medarbejder map(DTO.MedarbejderDTO medarbejder)
        {
            return new Medarbejder (medarbejder.ID, medarbejder.Navn, medarbejder.Admin,
                medarbejder.Telefonnr, medarbejder.Mail, medarbejder.Lærling);
        }
        //TODO mangler DTO
    }
}
