
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class Medarbejder
    {
        public Medarbejder(int iD, string navn, bool admin, string telefonnr, string mail, bool lærling)
        {
            ID = iD;
            Navn = navn;
            Admin = admin;
            Telefonnr = telefonnr;
            Mail = mail;
            Lærling = lærling;
        }

        public Medarbejder()
        {
        }

        [Key]
        public int ID { get; set; }

        public string Navn { get; set; } = string.Empty;

        public bool Admin { get; set; }

        public string Telefonnr { get; set; } = string.Empty;

        public string Mail { get; set; } = string.Empty;

        public bool Lærling { get; set; }

        public List<DagsProjekt> DagsProjekter { get; set; } = new();

        public List<LangtProjekt> LangtProjekter { get; set; } = new();
    }
}
