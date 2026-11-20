using BusinessLogic;
using DTO;

namespace TaekApp.Views;

public partial class OpretKundePage : ContentPage
{
    private readonly KundeService _kundeService = new();

    public OpretKundePage()
    {
        InitializeComponent();
    }

    private async void OnOpretKundeClicked(object sender, EventArgs e)
    {
        string navn = NavnEntry.Text?.Trim() ?? "";
        string adresse = AdresseEntry.Text?.Trim() ?? "";
        string telefon = TelefonEntry.Text?.Trim() ?? "";
        string mail = MailEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(navn))
        {
            await DisplayAlertAsync(
                "Manglende navn",
                "Indtast kundens navn.",
                "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(adresse))
        {
            await DisplayAlertAsync(
                "Manglende adresse",
                "Indtast kundens adresse.",
                "OK");
            return;
        }

        var kunde = new KundeDTO
        {
            Navn = navn,
            Adresse = adresse,
            Telefonnr = telefon,
            Mail = mail
        };

        var knap = (Button)sender;
        knap.IsEnabled = false;

        try
        {
            _kundeService.OpretKunde(kunde);
        }
        catch (Exception)
        {
            await DisplayAlertAsync(
                "Kunden blev ikke oprettet",
                "Der opstod en fejl under gemningen. Prøv igen.",
                "OK");
            return;
        }
        finally
        {
            knap.IsEnabled = true;
        }

        await DisplayAlertAsync(
            "Kunde oprettet",
            "Kunden er gemt.",
            "OK");

        await Navigation.PopAsync();
    }
}