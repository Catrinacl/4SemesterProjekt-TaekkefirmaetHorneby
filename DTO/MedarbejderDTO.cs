using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class MedarbejderDTO
    {
        public MedarbejderDTO(int iD, string navn, bool admin, string telefonnr, string mail, bool lærling)
        {
            ID = iD;
            Navn = navn;
            Admin = admin;
            Telefonnr = telefonnr;
            Mail = mail;
            Lærling = lærling;
        }
        public MedarbejderDTO() { 

        }
        public int ID { get; set; }
        public string Navn { get; set; } = string.Empty;
        public bool Admin { get; set; }
        public string Telefonnr { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public bool Lærling { get; set; }   

    }
}
