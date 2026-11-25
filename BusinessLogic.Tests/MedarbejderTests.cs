using DataAccess.Model;
using Xunit;

namespace BusinessLogic.Tests;

public class MedarbejderTests
{
    [Theory]
    [InlineData(1, "Anna", true, "12345678", "anna@test.dk", false)]
    [InlineData(2, "Bo", false, "87654321", "bo@test.dk", true)]
    public void Konstruktor_GemmerMedarbejderensOplysninger(
        int id,
        string navn,
        bool admin,
        string telefon,
        string mail,
        bool laerling)
    {
        // Act
        var medarbejder = new Medarbejder(
            id, navn, admin, telefon, mail, laerling);

        // Assert
        Assert.Equal(id, medarbejder.ID);
        Assert.Equal(navn, medarbejder.Navn);
        Assert.Equal(admin, medarbejder.Admin);
        Assert.Equal(telefon, medarbejder.Telefonnr);
        Assert.Equal(mail, medarbejder.Mail);
        Assert.Equal(laerling, medarbejder.Lærling);
    }
}