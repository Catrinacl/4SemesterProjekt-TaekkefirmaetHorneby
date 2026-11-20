using BusinessLogic;
using DTO;
namespace TaekApp.Views;

public partial class OpretMedarbejderPage : ContentPage
{
    private readonly MedarbejderService _medarbejderService = new();

    public OpretMedarbejderPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var opretNyMedarbejder = new MedarbejderDTO
        {
            Navn = medarbejderIndtast.Text ?? string.Empty,
            Admin = adminCheckBox.IsChecked,
            Telefonnr = telefonNrIndtast.Text ?? string.Empty,
            Mail = mailIndtast.Text ?? string.Empty,
            Lærling = laerlingCheckBox.IsChecked
        };

        _medarbejderService.OpretMedarbejder(opretNyMedarbejder);

        await Navigation.PopAsync();
    }
}