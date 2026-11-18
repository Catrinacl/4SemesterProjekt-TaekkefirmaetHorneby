using DTO;

namespace TaekApp.Views;

public partial class OpretProjektPage : ContentPage
{
    public static List<ProjektDTO> projektList = new List<ProjektDTO>();
    public OpretProjektPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		int projektID = int.Parse(projektInput.Text);
		int kundeID = int.Parse(kundeIdInput.Text);
		var medarbejdernavne = OpretMedarbejderPage.medarbejderList.Select(m => m.Navn).ToList();
        string adresse = adresseInput.Text;
		string status = statusInput.Text;
		int forventetVarighedTimer = int.Parse(forventetVarighedInput.Text);
		double kvadratMeterAfTag = int.Parse(kvadratMeterTagIndput.Text);


		var opretNytProjekt = new ProjektDTO
		{
			ProjektID = projektID,
			KundeID = kundeID,
			MedarbejderNavne = medarbejdernavne,
			Adresse = adresse,
			Status = status,
			ForventetVarighedTimer = forventetVarighedTimer,
			KvadratmeterAfTag = kvadratMeterAfTag
		};
		projektList.Add(opretNytProjekt);
        await Navigation.PushAsync(new OpretProjektPage());
    }

}