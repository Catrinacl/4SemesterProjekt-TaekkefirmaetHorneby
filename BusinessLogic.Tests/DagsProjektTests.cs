using DataAccess.Model;
using Xunit;

namespace BusinessLogic.Tests;

public class DagsProjektTests
{
    [Fact]
    public void Konstruktor_GemmerAdresseOgTagareal()
    {
        // Arrange
        string adresse = "Testvej 10";
        double tagareal = 25.5;

        // Act
        var projekt = new DagsProjekt(adresse, tagareal);

        // Assert
        Assert.Equal(adresse, projekt.Adress);
        Assert.Equal(tagareal, projekt.RoofSqrM);
    }

    [Fact]
    public void Konstruktor_SaetterStatusTilFremtidigtProjekt()
    {
        // Act
        var projekt = new DagsProjekt("Testvej 10", 25.5);

        // Assert
        Assert.Equal("Fremtidigt projekt", projekt.Status);
    }


    [Fact]
    public void NytDagsprojekt_KanTilfoejesEnArbejdsopgave()
    {
        // Arrange
        var projekt = new DagsProjekt("Testvej 10", 25.5);

        var arbejdsopgave = new Arbejdsopgave
        {
            Name = "Reparation af rygning"
        };

        // Act
        projekt.Arbejdsopgaver.Add(arbejdsopgave);

        // Assert
        var tilfoejetOpgave = Assert.Single(projekt.Arbejdsopgaver);
        Assert.Same(arbejdsopgave, tilfoejetOpgave);
    }
}