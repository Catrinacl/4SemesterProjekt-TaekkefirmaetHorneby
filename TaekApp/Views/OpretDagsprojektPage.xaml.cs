namespace TaekApp.Views;

public partial class OpretDagsprojektPage : ContentPage
{
	public OpretDagsprojektPage()
	{
		InitializeComponent();
	}
    private async void OnOpretDagsprojektClicked(object sender, EventArgs e)
    {
        string adresse = AdresseEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(adresse))
        {
            await DisplayAlertAsync("Manglende adresse",
                "Indtast en adresse.", "OK");
            return;
        }

        bool gyldigtTal = double.TryParse(
            TagarealEntry.Text,
            out double tagareal);

        if (!gyldigtTal || !double.IsFinite(tagareal) || tagareal <= 0)
        {
            await DisplayAlertAsync("Ugyldigt tagareal",
                "Indtast et tagareal større end 0.", "OK");
            return;
        }

        await DisplayAlertAsync("Oplysningerne er gyldige",
            $"Adresse: {adresse}\nTagareal: {tagareal} m²",
            "OK");
    }
}