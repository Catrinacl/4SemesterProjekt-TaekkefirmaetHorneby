using DTO;

namespace TaekApp.Views;

public partial class OpretDagsprojektPage : ContentPage
{
    public OpretDagsprojektPage()
    {
        InitializeComponent();

        StartdatoPicker.Date = DateTime.Today;
        SlutdatoPicker.Date = DateTime.Today;
    }

    private async void OnOpretDagsprojektClicked(object sender, EventArgs e)
    {
        string adresse = AdresseEntry.Text?.Trim() ?? "";
        string status = StatusEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(adresse))
        {
            await DisplayAlertAsync(
                "Manglende adresse",
                "Indtast en adresse.",
                "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(status))
        {
            await DisplayAlertAsync(
                "Manglende status",
                "Indtast en status.",
                "OK");
            return;
        }

        if (!double.TryParse(VarighedEntry.Text, out double varighed)
            || !double.IsFinite(varighed)
            || varighed <= 0)
        {
            await DisplayAlertAsync(
                "Ugyldig varighed",
                "Indtast et antal timer større end 0.",
                "OK");
            return;
        }

        if (!double.TryParse(TagarealEntry.Text, out double tagareal)
            || !double.IsFinite(tagareal)
            || tagareal <= 0)
        {
            await DisplayAlertAsync(
                "Ugyldigt tagareal",
                "Indtast et tagareal større end 0.",
                "OK");
            return;
        }

        if (StartdatoPicker.Date is not DateTime startdato
            || SlutdatoPicker.Date is not DateTime slutdato)
        {
            await DisplayAlertAsync(
                "Manglende dato",
                "Vælg både startdato og slutdato.",
                "OK");
            return;
        }

        if (slutdato.Date < startdato.Date)
        {
            await DisplayAlertAsync(
                "Ugyldige datoer",
                "Slutdato må ikke være før startdato.",
                "OK");
            return;
        }

        var dagsprojekt = new OpretDagsprojektDTO
        {
            Adress = adresse,
            Status = status,
            ExpectedDurationHours = varighed,
            RoofSqrM = tagareal,
            Startdate = startdato.Date,
            Endate = slutdato.Date
        };

        await DisplayAlertAsync(
            "Oplysningerne er gyldige",
            $"Adresse: {dagsprojekt.Adress}\n" +
            $"Status: {dagsprojekt.Status}\n" +
            $"Varighed: {dagsprojekt.ExpectedDurationHours} timer\n" +
            $"Tagareal: {dagsprojekt.RoofSqrM} m²\n" +
            $"Start: {dagsprojekt.Startdate:dd.MM.yyyy}\n" +
            $"Slut: {dagsprojekt.Endate:dd.MM.yyyy}",
            "OK");
    }
}