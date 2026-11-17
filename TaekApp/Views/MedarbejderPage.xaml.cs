using DTO;

namespace TaekApp.Views;

public partial class MedarbejderPage : ContentView
{
	public MedarbejderPage()
	{
		InitializeComponent();
	}

    public void TestDataMedarbejdere()
    {
        List<MedarbejderDTO> testMedarbejdere = new List<MedarbejderDTO>
        {
            new MedarbejderDTO { Navn = "Cat", Admin=true, Telefonnr = "12345678", Mail = "cat@mail.dk", Lærling = false },

            new MedarbejderDTO { Navn = "Cat", Admin=true, Telefonnr = "12345678", Mail = "cat@mail.dk", Lærling = false }

        };
        collectionViewMedarbejdere.ItemsSource = testMedarbejdere;

    }


}