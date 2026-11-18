using DataAccess.Model;
using DTO;

namespace TaekApp.Views;

public partial class MedarbejderPage : ContentView
{
    

    public MedarbejderPage()
	{
		InitializeComponent();
        TestDataMedarbejdere();

        collectionViewMedarbejdere.ItemsSource = OpretMedarbejderPage.medarbejderList;
	}

    public void TestDataMedarbejdere()
    {
        List<MedarbejderDTO> testMedarbejdere = new List<MedarbejderDTO>
        {
            new MedarbejderDTO { Navn = "Cat", Admin=true, Telefonnr = "12345678", Mail = "cat@mail.dk", Lærling = false },
            new MedarbejderDTO { Navn = "Cat 2", Admin=false, Telefonnr = "88888888", Mail = "cat2mail.dk", Lærling = true }

        };
        collectionViewMedarbejdere.ItemsSource = testMedarbejdere;

    }
}