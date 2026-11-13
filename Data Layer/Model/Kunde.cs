using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model;

public class Kunde
{
    public int Kundenummer { get; set; }
    public string Navn { get; set; } = string.Empty;
    public string Adresse { get; set; } = string.Empty;
    public string Telefonnr { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
}