using DTO;
namespace TaekApp.Views;

public partial class OpretMedarbejderPage : ContentPage
{
  public static List<MedarbejderDTO> medarbejderList = new List<MedarbejderDTO>();
    public OpretMedarbejderPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		int id = int.Parse(idIndtast.Text);
		string navn = medarbejderIndtast.Text;
        bool admin = false;
		string telefonNr = telefonNrIndtast.Text;
		string mail = mailIndtast.Text;
		bool lærling = false;


		var opretNyMedarbejder = new MedarbejderDTO
		{
			ID = id,
			Navn = navn,
			Admin = admin,
			Telefonnr = telefonNr,
			Mail = mail,
			Lærling = lærling
		};

		medarbejderList.Add(opretNyMedarbejder);
		await Navigation.PushAsync(new OpretMedarbejderPage());
    }
}