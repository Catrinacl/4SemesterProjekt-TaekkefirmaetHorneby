using BusinessLogic;
using DataAccess.Model;
using DTO;
using Xunit;

namespace BusinessLogic.Tests;

public class ProjektBeregnerTests
{
    [Fact]
    public void Beregn_20KvadratmeterMedEnSvend_Returnerer15Timer()
    {
        // Arrange
        var beregner = new ProjektBeregner();

        var opgaver = new List<Arbejdsopgave>
        {
            new Arbejdsopgave
            {
                ArbejdsopgaveID = 1,
                Name = "Tækning",
                Unit = "m²",
                SvendProductionPerDay = 10
            }
        };

        var valgteOpgaver = new List<ValgtArbejdsopgaveDTO>
        {
            new ValgtArbejdsopgaveDTO
            {
                ArbejdsopgaveID = 1,
                Maengde = 20
            }
        };

        var medarbejdere = new List<Medarbejder>
        {
            new Medarbejder
            {
                Navn = "Testmedarbejder",
                Lærling = false
            }
        };

        // Act
        var resultat = beregner.Beregn(
            valgteOpgaver,
            opgaver,
            medarbejdere,
            new DateTime(2026, 11, 23));

        // Assert
        Assert.Equal(
            15.0,
            resultat.ForventetVarighedTimer,
            precision: 5);
    }
    [Fact]
    public void Beregn_20KvadratmeterMedEnLaerling_Returnerer30Timer()
    {
        // Arrange
        var beregner = new ProjektBeregner();

        var opgaver = new List<Arbejdsopgave>
    {
        new Arbejdsopgave
        {
            ArbejdsopgaveID = 1,
            Name = "Tækning",
            Unit = "m²",
            SvendProductionPerDay = 10,
            LaerlingProductionPerDay = 5
        }
    };

        var valgteOpgaver = new List<ValgtArbejdsopgaveDTO>
    {
        new ValgtArbejdsopgaveDTO
        {
            ArbejdsopgaveID = 1,
            Maengde = 20
        }
    };

        var medarbejdere = new List<Medarbejder>
    {
        new Medarbejder { Lærling = true }
    };

        // Act
        var resultat = beregner.Beregn(
            valgteOpgaver,
            opgaver,
            medarbejdere,
            new DateTime(2026, 11, 23));

        // Assert
        Assert.Equal(
            30.0,
            resultat.ForventetVarighedTimer,
            precision: 5);
    }
    [Fact]
    public void Beregn_UdenMedarbejdere_KasterInvalidOperationException()
    {
        // Arrange
        var beregner = new ProjektBeregner();

        var opgaver = new List<Arbejdsopgave>
    {
        new Arbejdsopgave
        {
            ArbejdsopgaveID = 1,
            SvendProductionPerDay = 10
        }
    };

        var valgteOpgaver = new List<ValgtArbejdsopgaveDTO>
    {
        new ValgtArbejdsopgaveDTO
        {
            ArbejdsopgaveID = 1,
            Maengde = 20
        }
    };

        var medarbejdere = new List<Medarbejder>();

        // Act og Assert
        Assert.Throws<InvalidOperationException>(() =>
            beregner.Beregn(
                valgteOpgaver,
                opgaver,
                medarbejdere,
                new DateTime(2026, 11, 23)));
    }

    [Fact]
    public void Beregn_ToArbejdsdageFraFredag_GiverSlutdatoMandag()
    {
        // Arrange
        var beregner = new ProjektBeregner();

        var opgaver = new List<Arbejdsopgave>
    {
        new Arbejdsopgave
        {
            ArbejdsopgaveID = 1,
            Name = "Tækning",
            SvendProductionPerDay = 10
        }
    };

        var valgteOpgaver = new List<ValgtArbejdsopgaveDTO>
    {
        new ValgtArbejdsopgaveDTO
        {
            ArbejdsopgaveID = 1,
            Maengde = 20
        }
    };

        var medarbejdere = new List<Medarbejder>
    {
        new Medarbejder { Lærling = false }
    };

        var fredag = new DateTime(2026, 11, 27);
        var forventetSlutdato = new DateTime(2026, 11, 30);

        // Act
        var resultat = beregner.Beregn(
            valgteOpgaver,
            opgaver,
            medarbejdere,
            fredag);

        // Assert
        Assert.Equal(
            15.0,
            resultat.ForventetVarighedTimer,
            precision: 5);

        Assert.Equal(forventetSlutdato, resultat.Slutdato);
    }
    [Fact]
    public void Beregn_MedSvendOgLaerling_LaeggerProduktionenSammen()
    {
        // Arrange
        var beregner = new ProjektBeregner();

        var opgaver = new List<Arbejdsopgave>
    {
        new Arbejdsopgave
        {
            ArbejdsopgaveID = 1,
            Name = "Tækning",
            SvendProductionPerDay = 10,
            LaerlingProductionPerDay = 5
        }
    };

        var valgteOpgaver = new List<ValgtArbejdsopgaveDTO>
    {
        new ValgtArbejdsopgaveDTO
        {
            ArbejdsopgaveID = 1,
            Maengde = 30
        }
    };

        var medarbejdere = new List<Medarbejder>
    {
        new Medarbejder { Lærling = false },
        new Medarbejder { Lærling = true }
    };

        // Act
        var resultat = beregner.Beregn(
            valgteOpgaver,
            opgaver,
            medarbejdere,
            new DateTime(2026, 11, 23));

        // Assert
        Assert.Equal(
            15.0,
            resultat.ForventetVarighedTimer,
            precision: 5);
    }
}