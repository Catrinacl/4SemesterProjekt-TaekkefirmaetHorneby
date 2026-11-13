using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model;

public class Medarbejder
{
    public int ID { get; set; }
    public string Navn { get; set; } = string.Empty;
    public bool Admin { get; set; }
    public string Telefonnr { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;
    public bool Lærling { get; set; }
}