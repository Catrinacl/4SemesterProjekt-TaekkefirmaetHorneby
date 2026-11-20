using BusinessLogic;
using DTO;

namespace TaekApp.Views;

public partial class OpretProjektPage : ContentPage
{
    public static List<ProjektDTO> projektList = new List<ProjektDTO>();
    private readonly MedarbejderService _medarbejderService = new();
    public OpretProjektPage()
	{
		InitializeComponent();
        MedListe.ItemsSource = _medarbejderService.GetAlleMedarbejdere();
        MedListe.ItemDisplayBinding = new Binding("Navn");
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
		int.TryParse(projektInput.Text, out int projektID);
		int.TryParse(kundeIdInput.Text, out int kundeID);
		int.TryParse(forventetVarighedInput.Text, out int forventetVarighedTimer);
		double.TryParse(kvadratMeterTagIndput.Text, out double kvadratMeterAfTag);
        
		var valgteMedarbejdere = new List<String>();
        if (MedListe.SelectedItem is MedarbejderDTO valgtMedarbejder)
        {
            valgteMedarbejdere.Add(valgtMedarbejder.Navn);
        }

        var opretNytProjekt = new ProjektDTO
		{
			ProjektID = projektID,
			KundeID = kundeID,
			MedarbejderNavne = valgteMedarbejdere,
			Adresse = adresseInput.Text,
			Status = statusInput.Text,
			ForventetVarighedTimer = forventetVarighedTimer,
			KvadratmeterAfTag = kvadratMeterAfTag
		};
		projektList.Add(opretNytProjekt);
		await Navigation.PopAsync();
    }

}