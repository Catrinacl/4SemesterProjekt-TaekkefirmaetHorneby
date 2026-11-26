
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BusinessLogic;
using DTO;
using Microsoft.Maui.Controls;

namespace TaekApp.Views;

public partial class OpretDagsprojektPage : ContentPage
{
    private readonly ArbejdsopgaveService _arbejdsopgaveService = new();
    private readonly DagsprojektService _dagsprojektService = new();
    private readonly KundeService _kundeService = new();
    private readonly MedarbejderService _medarbejderService = new();

    private readonly List<(
        OpretArbejdsopgaveDTO Opgave,
        CheckBox Valgt,
        Entry Maengde,
        Entry ManuelTid)> _visteArbejdsopgaver = new();

    private readonly Dictionary<int, CheckBox> _medarbejderValg = new();

    private bool _beregningErAktuel;

    public OpretDagsprojektPage()
    {
        InitializeComponent();

        StartdatoPicker.Date = DateTime.Today;

        HentOgVisKunder();
        HentOgVisMedarbejdere();
        HentOgVisArbejdsopgaver();

        StartdatoPicker.DateSelected += (_, _) => NulstilBeregning();
        KundeListe.SelectedIndexChanged += (_, _) => NulstilBeregning();
        AdresseEntry.TextChanged += (_, _) => NulstilBeregning();
        StatusEntry.TextChanged += (_, _) => NulstilBeregning();
        TagarealEntry.TextChanged += (_, _) => NulstilBeregning();
    }

    private void HentOgVisKunder()
    {
        try
        {
            var kunder = _kundeService.GetAlleKunder();

            KundeListe.ItemsSource = kunder;
            KundeListe.ItemDisplayBinding = new Binding("Navn");
        }
        catch (Exception ex)
        {
            DisplayAlert("Fejl", $"Kunne ikke hente kunder: {ex.Message}", "OK");
        }
    }

    private void HentOgVisMedarbejdere()
    {
        try
        {
            var medarbejdere = _medarbejderService.GetAlleMedarbejdere();

            MedarbejderContainer.Children.Clear();
            _medarbejderValg.Clear();

            foreach (var medarbejder in medarbejdere)
            {
                var checkBox = new CheckBox
                {
                    VerticalOptions = LayoutOptions.Center
                };

                checkBox.CheckedChanged += (_, _) => NulstilBeregning();

                var navnLabel = new Label
                {
                    Text = medarbejder.Lærling
                        ? $"{medarbejder.Navn} (Lærling)"
                        : $"{medarbejder.Navn} (Svend)",
                    TextColor = Color.FromArgb("#4A4946"),
                    VerticalOptions = LayoutOptions.Center
                };

                var række = new HorizontalStackLayout
                {
                    Spacing = 10,
                    Children = { checkBox, navnLabel }
                };

                MedarbejderContainer.Children.Add(række);
                _medarbejderValg.Add(medarbejder.ID, checkBox);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Fejl", $"Kunne ikke hente medarbejdere: {ex.Message}", "OK");
        }
    }

    private void HentOgVisArbejdsopgaver()
    {
        try
        {
            var arbejdsopgaver = _arbejdsopgaveService.GetAlleArbejdsopgaver();

            ArbejdsopgaverContainer.Children.Clear();
            _visteArbejdsopgaver.Clear();

            foreach (var opgave in arbejdsopgaver)
            {
                var checkBox = new CheckBox
                {
                    VerticalOptions = LayoutOptions.Center
                };

                var navnLabel = new Label
                {
                    Text = $"{opgave.Name} ({opgave.Unit})",
                    TextColor = Color.FromArgb("#4A4946"),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var maengdeEntry = new Entry
                {
                    Placeholder = "Mængde",
                    Keyboard = Keyboard.Numeric,
                    WidthRequest = 110,
                    IsEnabled = false,
                    IsVisible = !opgave.RequiresManualTime,
                    TextColor = Color.FromArgb("#4A4946")
                };

                var manuelTidEntry = new Entry
                {
                    Placeholder = "Timer",
                    Keyboard = Keyboard.Numeric,
                    WidthRequest = 110,
                    IsEnabled = false,
                    IsVisible = opgave.RequiresManualTime,
                    TextColor = Color.FromArgb("#4A4946")
                };

                checkBox.CheckedChanged += (_, e) =>
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
                    Children =
                    {
                        checkBox,
                        navnLabel,
                        maengdeEntry,
                        manuelTidEntry
                    }
                };

                ArbejdsopgaverContainer.Children.Add(række);

                _visteArbejdsopgaver.Add(
                    (opgave, checkBox, maengdeEntry, manuelTidEntry));
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Fejl", $"Kunne ikke hente arbejdsopgaver: {ex.Message}", "OK");
        }
    }

    private void NulstilBeregning()
    {
        _beregningErAktuel = false;

        VarighedEntry.Text = string.Empty;
        SlutdatoResultat.Text = string.Empty;

        OpretDagsprojektButton.IsEnabled = false;
    }

    private OpretDagsprojektDTO BygDagsprojektDTO()
    {
        if (KundeListe.SelectedItem is not KundeDTO valgtKunde)
            throw new InvalidOperationException("Vælg venligst en kunde.");

        string adresse = AdresseEntry.Text?.Trim() ?? "";
        string status = StatusEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(adresse))
            throw new InvalidOperationException("Indtast en adresse.");

        if (string.IsNullOrWhiteSpace(status))
            throw new InvalidOperationException("Indtast en status.");

        if (!double.TryParse(
                TagarealEntry.Text,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out double tagareal)
            || !double.IsFinite(tagareal)
            || tagareal <= 0)
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

        var valgteArbejdsopgaver = new List<ValgtArbejdsopgaveDTO>();

        foreach (var vistOpgave in _visteArbejdsopgaver)
        {
            if (!vistOpgave.Valgt.IsChecked)
                continue;

            double maengde = 0;
            double? manuelTid = null;

            if (vistOpgave.Opgave.RequiresManualTime)
            {
                if (!double.TryParse(
                        vistOpgave.ManuelTid.Text,
                        NumberStyles.Number,
                        CultureInfo.CurrentCulture,
                        out double timer)
                    || !double.IsFinite(timer)
                    || timer <= 0)
                {
                    throw new InvalidOperationException(
                        $"Indtast et gyldigt antal timer for {vistOpgave.Opgave.Name}.");
                }

                manuelTid = timer;
            }
            else
            {
                if (!double.TryParse(
                        vistOpgave.Maengde.Text,
                        NumberStyles.Number,
                        CultureInfo.CurrentCulture,
                        out maengde)
                    || !double.IsFinite(maengde)
                    || maengde <= 0)
                {
                    throw new InvalidOperationException(
                        $"Indtast en gyldig mængde for {vistOpgave.Opgave.Name}.");
                }
            }

            valgteArbejdsopgaver.Add(new ValgtArbejdsopgaveDTO
            {
                ArbejdsopgaveID = vistOpgave.Opgave.ArbejdsopgaveID,
                Maengde = maengde,
                ManualTimeHours = manuelTid
            });
        }

        if (valgteArbejdsopgaver.Count == 0)
            throw new InvalidOperationException(
                "Vælg mindst én arbejdsopgave.");

        return new OpretDagsprojektDTO
        {
            KundeID = valgtKunde.Kundenummer,
            Adress = adresse,
            Status = status,
            RoofSqrM = tagareal,
            Startdate = StartdatoPicker.Date ?? DateTime.Today,
            MedarbejderIds = medarbejderIds,
            Arbejdsopgaver = valgteArbejdsopgaver
        };
    }

    private async void OnBeregnDagsprojektClicked(
        object sender,
        EventArgs e)
    {
        try
        {
            NulstilBeregning();

            var dagsprojekt = BygDagsprojektDTO();

            var resultat = _dagsprojektService.BeregnForhaandsvisning(
                dagsprojekt.MedarbejderIds,
                dagsprojekt.Arbejdsopgaver,
                dagsprojekt.Startdate);

            VarighedEntry.Text = resultat.ForventetVarighedTimer.ToString(
                "F1",
                CultureInfo.CurrentCulture);

            SlutdatoResultat.Text =
                resultat.Slutdato.ToString("dd-MM-yyyy");

            _beregningErAktuel = true;
            OpretDagsprojektButton.IsEnabled = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Kunne ikke beregne dagsprojekt",
                ex.Message,
                "OK");
        }
    }

    private async void OnOpretDagsprojektClicked(
        object sender,
        EventArgs e)
    {
        if (!_beregningErAktuel)
        {
            await DisplayAlert(
                "Beregn først",
                "Tryk på 'Beregn dagsprojekt', før du opretter projektet.",
                "OK");
            return;
        }

        try
        {
            var dagsprojekt = BygDagsprojektDTO();

            OpretDagsprojektButton.IsEnabled = false;

            _dagsprojektService.OpretNytDagsProjekt(dagsprojekt);

            await DisplayAlert(
                "Projekt oprettet",
                $"Dagsprojektet er gemt.\n" +
                $"Forventet arbejdstid: {VarighedEntry.Text} timer.\n" +
                $"Slutdato: {SlutdatoResultat.Text}",
                "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            OpretDagsprojektButton.IsEnabled = true;

            await DisplayAlert(
                "Fejl ved oprettelse",
                ex.Message,
                "OK");
        }
    }
}
