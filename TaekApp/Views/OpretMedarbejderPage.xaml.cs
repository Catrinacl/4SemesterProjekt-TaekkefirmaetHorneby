using DTO;
namespace TaekApp.Views;

public partial class OpretMedarbejderPage : ContentPage
{
    public static List<MedarbejderDTO> medarbejderList = new List<MedarbejderDTO>
    {
        new MedarbejderDTO { ID = 1, Navn = "Cat", Admin = true, Telefonnr = "12345678", Mail = "cat@mail.dk", Lærling = false },
        new MedarbejderDTO { ID = 2, Navn = "Cat 2", Admin = false, Telefonnr = "88888888", Mail = "cat2@mail.dk", Lærling = true }
    };

    public OpretMedarbejderPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        int.TryParse(idIndtast.Text, out int id);

        var opretNyMedarbejder = new MedarbejderDTO
        {
            ID = id,
            Navn = medarbejderIndtast.Text ?? string.Empty,
            Admin = adminCheckBox.IsChecked,
            Telefonnr = telefonNrIndtast.Text ?? string.Empty,
            Mail = mailIndtast.Text ?? string.Empty,
            Lærling = laerlingCheckBox.IsChecked
        };

        medarbejderList.Add(opretNyMedarbejder);
        await Navigation.PopAsync();
    }
}