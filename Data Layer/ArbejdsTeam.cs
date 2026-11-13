using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Model;

public class ArbejdsTeam
{
    public int ID { get; set; }

    public List<Medarbejder> Medarbejdere { get; set; } = new();
}