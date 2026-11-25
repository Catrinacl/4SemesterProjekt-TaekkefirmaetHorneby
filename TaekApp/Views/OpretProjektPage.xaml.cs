
using BusinessLogic;
using DTO;
using System.Globalization;

namespace TaekApp.Views;

public partial class OpretProjektPage : ContentPage
{
    public static List<ProjektDTO> projektList = new();

    private readonly MedarbejderService _medarbejderService = new();
    private readonly ArbejdsopgaveService _arbejdsopgaveService = new();
    private readonly KundeService _kundeService = new();
    private readonly LangtProjektService _langtProjektService = new();

    private readonly Dictionary<int, CheckBox> _medarbejderValg = new();
    private readonly Dictionary<int, CheckBox> _opgaveValg = new();
    private readonly Dictionary<int, Entry> _opgaveMaengder = new();
    private readonly Dictionary<int, Entry> _opgaveManuelTid = new();

    private bool _beregningErAktuel;

    public OpretProjektPage()
    {
        InitializeComponent();

        StartDatoPicker.Date = DateTime.Today;

        IndlaesKunder();
        IndlaesMedarbejdere();
        IndlaesArbejdsopgaver();

        StartDatoPicker.DateSelected += (_, _) => NulstilBeregning();
        KundeListe.SelectedIndexChanged += (_, _) => NulstilBeregning();
        adresseInput.TextChanged += (_, _) => NulstilBeregning();
        statusInput.TextChanged += (_, _) => NulstilBeregning();
        kvadratMeterTagIndput.TextChanged += (_, _) => NulstilBeregning();
    }

    private async void IndlaesKunder()
    {
        try
        {
            var kunder = await Task.Run(
                () => _kundeService.GetAlleKunder());

            KundeListe.ItemsSource = kunder;
            KundeListe.ItemDisplayBinding = new Binding("Navn");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fejl", ex.Message, "OK");
        }
    }

    private async void IndlaesMedarbejdere()
    {
        try
        {
            var medarbejdere = await Task.Run(
                () => _medarbejderService.GetAlleMedarbejdere());

            MedarbejderListe.Children.Clear();
            _medarbejderValg.Clear();

            foreach (var medarbejder in medarbejdere)
            {
                var checkbox = new CheckBox
                {
                    VerticalOptions = LayoutOptions.Center
                };

                checkbox.CheckedChanged += (_, _) => NulstilBeregning();

                var label = new Label
                {
                    Text = medarbejder.Lærling
                        ? $"{medarbejder.Navn} (Lærling)"
                        : $"{medarbejder.Navn} (Svend)",
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#4A4946")
                };

                var række = new HorizontalStackLayout
                {
                    Spacing = 10
                };

                række.Children.Add(checkbox);
                række.Children.Add(label);

                MedarbejderListe.Children.Add(række);

                _medarbejderValg.Add(medarbejder.ID, checkbox);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fejl", ex.Message, "OK");
        }
    }

    private async void IndlaesArbejdsopgaver()
    {
        try
        {
            var arbejdsopgaver = await Task.Run(
                () => _arbejdsopgaveService.GetAlleArbejdsopgaver());

            ArbejdsopgaveListe.Children.Clear();

            _opgaveValg.Clear();
            _opgaveMaengder.Clear();
            _opgaveManuelTid.Clear();

            foreach (var opgave in arbejdsopgaver)
            {
                var checkbox = new CheckBox
                {
                    VerticalOptions = LayoutOptions.Center
                };

                var navnLabel = new Label
                {
                    Text = $"{opgave.Name} ({opgave.Unit})",
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromArgb("#4A4946")
                };

                var maengdeEntry = new Entry
                {
                    Placeholder = "Mængde",
                    Keyboard = Keyboard.Numeric,
                    IsEnabled = false,
                    WidthRequest = 110,
                    BackgroundColor = Color.FromArgb("#FAF9F6"),
                    TextColor = Color.FromArgb("#4A4946")
                };

                var manuelTidEntry = new Entry
                {
                    Placeholder = "Timer",
                    Keyboard = Keyboard.Numeric,
                    IsEnabled = false,
                    WidthRequest = 110,
                    BackgroundColor = Color.FromArgb("#FAF9F6"),
                    TextColor = Color.FromArgb("#4A4946"),
                    IsVisible = opgave.RequiresManualTime
                };

                checkbox.CheckedChanged += (_, e) =>
                {
                    maengdeEntry.IsEnabled =
                        e.Value && !opgave.RequiresManualTime;

                    manuelTidEntry.IsEnabled =
                        e.Value && opgave.RequiresManualTime;

                    if (!e.Value)
                    {
                        maengdeEntry.Text = string.Empty;
                        manuelTidEntry.Text = string.Empty;
                    }

                    NulstilBeregning();
                };

                maengdeEntry.TextChanged += (_, _) => NulstilBeregning();
                manuelTidEntry.TextChanged += (_, _) => NulstilBeregning();

                var række = new HorizontalStackLayout
                {
                    Spacing = 10,
                    VerticalOptions = LayoutOptions.Center
                };

                række.Children.Add(checkbox);
                række.Children.Add(navnLabel);

                if (opgave.RequiresManualTime)
                    række.Children.Add(manuelTidEntry);
                else
                    række.Children.Add(maengdeEntry);

                ArbejdsopgaveListe.Children.Add(række);

                _opgaveValg.Add(opgave.ArbejdsopgaveID, checkbox);
                _opgaveMaengder.Add(opgave.ArbejdsopgaveID, maengdeEntry);
                _opgaveManuelTid.Add(opgave.ArbejdsopgaveID, manuelTidEntry);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fejl", ex.Message, "OK");
        }
    }

    private void NulstilBeregning()
    {
        _beregningErAktuel = false;

        forventetVarighedInput.Text = string.Empty;
        SlutdatoResultat.Text = string.Empty;

        OpretButton.IsEnabled = false;
    }

    private bool ForsøgAtByggeProjektDTO(out OpretProjektDTO projekt)
    {
        projekt = new OpretProjektDTO();

        if (KundeListe.SelectedItem is not KundeDTO valgtKunde)
            throw new InvalidOperationException("Vælg venligst en kunde.");

        string adresse = adresseInput.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(adresse))
            throw new InvalidOperationException("Indtast projektets adresse.");

        if (!double.TryParse(
            kvadratMeterTagIndput.Text,
            NumberStyles.Number,
            CultureInfo.CurrentCulture,
            out double tagareal) ||
            !double.IsFinite(tagareal) ||
            tagareal <= 0)
        {
            throw new InvalidOperationException(
                "Indtast et gyldigt tagareal større end 0.");
        }

        var medarbejderIds = _medarbejderValg
            .Where(m => m.Value.IsChecked)
            .Select(m => m.Key)
            .ToList();

        if (medarbejderIds.Count == 0)
            throw new InvalidOperationException("Vælg mindst én medarbejder.");

        var valgteOpgaver = new List<ValgtArbejdsopgaveDTO>();

        foreach (var opgaveID in _opgaveValg.Keys)
        {
            if (!_opgaveValg[opgaveID].IsChecked)
                continue;

            double maengde = 0;
            double? manuelTid = null;

            if (!string.IsNullOrWhiteSpace(_opgaveMaengder[opgaveID].Text))
            {
                if (!double.TryParse(
                    _opgaveMaengder[opgaveID].Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out maengde) ||
                    !double.IsFinite(maengde) ||
                    maengde <= 0)
                {
                    throw new InvalidOperationException(
                        "Kontroller mængden på dine arbejdsopgaver.");
                }
            }

            if (!string.IsNullOrWhiteSpace(_opgaveManuelTid[opgaveID].Text))
            {
                if (!double.TryParse(
                    _opgaveManuelTid[opgaveID].Text,
                    NumberStyles.Number,
                    CultureInfo.CurrentCulture,
                    out double timer) ||
                    !double.IsFinite(timer) ||
                    timer <= 0)
                {
                    throw new InvalidOperationException(
                        "Kontroller den manuelt indtastede tid.");
                }

                manuelTid = timer;
            }

            valgteOpgaver.Add(new ValgtArbejdsopgaveDTO
            {
                ArbejdsopgaveID = opgaveID,
                Maengde = maengde,
                ManualTimeHours = manuelTid
            });
        }

        if (valgteOpgaver.Count == 0)
            throw new InvalidOperationException("Vælg mindst én arbejdsopgave.");

        projekt = new OpretProjektDTO
        {
            KundeID = valgtKunde.Kundenummer,
            Adresse = adresse,
            Status = string.IsNullOrWhiteSpace(statusInput.Text)
                ? "Fremtidigt projekt"
                : statusInput.Text.Trim(),
            KvadratmeterAfTag = tagareal,
            Startdate = StartDatoPicker.Date ?? DateTime.Today,
            MedarbejderIds = medarbejderIds,
            Arbejdsopgaver = valgteOpgaver
        };

        return true;
    }

    private async void BeregnProjekt_Clicked(object sender, EventArgs e)
    {
        try
        {
            NulstilBeregning();

            ForsøgAtByggeProjektDTO(out var projekt);

            var resultat = await Task.Run(() =>
                _langtProjektService.BeregnForhaandsvisning(
                    projekt.MedarbejderIds,
                    projekt.Arbejdsopgaver,
                    projekt.Startdate));

            forventetVarighedInput.Text =
                resultat.ForventetVarighedTimer.ToString(
                    "F1",
                    CultureInfo.CurrentCulture);

            SlutdatoResultat.Text =
                resultat.Slutdato.ToString("dd-MM-yyyy");

            _beregningErAktuel = true;
            OpretButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Kunne ikke beregne projekt", ex.Message, "OK");
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (!_beregningErAktuel)
        {
            await DisplayAlert(
                "Beregn projektet først",
                "Tryk på 'Beregn projekt', før du opretter projektet.",
                "OK");
            return;
        }

        try
        {
            ForsøgAtByggeProjektDTO(out var nytProjekt);

            OpretButton.IsEnabled = false;

            double forventetTid = await Task.Run(() =>
                _langtProjektService.OpretNytProjekt(nytProjekt));

            await DisplayAlert(
                "Projekt oprettet",
                $"Projektet er gemt.\nForventet arbejdstid: {forventetTid:F1} timer.\nSlutdato: {SlutdatoResultat.Text}",
                "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            OpretButton.IsEnabled = true;

            await DisplayAlert(
                "Kunne ikke oprette projekt",
                ex.Message,
                "OK");
        }
    }
}
