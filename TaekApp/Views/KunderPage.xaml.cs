using DTO;
using BusinessLogic;

namespace TaekApp.Views;

public partial class KunderPage : ContentView
{
    public KunderPage()
	{
		InitializeComponent();
		TestDataKunder();
		
	}

    public void TestDataKunder()
    {
        List<KundeDTO> testKunder = new List<KundeDTO>
        {
            new KundeDTO { Navn = "Cat", Adresse = "Vej 1", Telefonnr = "12345678", Mail = "cat@mail.dk" },
            new KundeDTO { Navn = "Cat 2", Adresse = "Vej 2", Telefonnr = "88888888", Mail = "cat2@mail.dk" },
            new KundeDTO { Navn = "Cat 3", Adresse = "Vej 3", Telefonnr = "27218792", Mail = "cat3@mail.dk" }
        };

        collectionViewKunder.ItemsSource = testKunder;
    }
}