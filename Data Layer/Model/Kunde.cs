using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model;

public class Kunde
{
    [Key]
    public int Kundenummer { get; set; }

    public string Navn { get; set; } = string.Empty;
    public string Adresse { get; set; } = string.Empty;
    public string Telefonnr { get; set; } = string.Empty;
    public string Mail { get; set; } = string.Empty;

    public List<DagsProjekt> DagsProjekter { get; set; } = new();
    public List<LangtProjekt> LangtProjekter { get; set; } = new();
}