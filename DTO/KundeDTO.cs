using System;
using System.Collections.Generic;
using System.Text;


namespace DTO
{
    public class KundeDTO
    {
        public int Kundenummer { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telefonnr { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
    }
}
