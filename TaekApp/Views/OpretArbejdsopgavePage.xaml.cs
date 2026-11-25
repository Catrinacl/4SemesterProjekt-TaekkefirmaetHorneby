
using BusinessLogic;
using DTO;

namespace TaekApp.Views;

public partial class OpretArbejdsopgavePage : ContentPage
{
    private readonly ArbejdsopgaveService _service =
        new ArbejdsopgaveService();

    public OpretArbejdsopgavePage()
    {
        InitializeComponent();
    }

    private async void OnOpretArbejdsopgaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNavn.Text))
        {
            await DisplayAlert("Fejl", "Du skal indtaste et navn.", "OK");
            return;
        }

        if (!double.TryParse(txtSvendProduktion.Text, out double svendProduktion) ||
            !double.TryParse(txtLaerlingProduktion.Text, out double laerlingProduktion))
        {
            await DisplayAlert("Fejl", "Indtast gyldige produktionstal.", "OK");
            return;
        }

        var nyArbejdsopgave = new OpretArbejdsopgaveDTO
        {
            Name = txtNavn.Text,
            Description = txtBeskrivelse.Text ?? string.Empty,
            Unit = txtEnhed.Text ?? string.Empty,
            SvendProductionPerDay = svendProduktion,
            LaerlingProductionPerDay = laerlingProduktion
        };

        _service.OpretArbejdsopgave(nyArbejdsopgave);

        await DisplayAlert("Gemt", "Arbejdsopgaven er oprettet.", "OK");
    }
}
