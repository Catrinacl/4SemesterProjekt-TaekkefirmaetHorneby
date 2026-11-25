
using System;
using System.Collections.Generic;
using BusinessLogic;
using DTO;
using Microsoft.Maui.Controls;

namespace TaekApp.Views;

public partial class OpretDagsprojektPage : ContentPage
{
    private readonly ArbejdsopgaveService _arbejdsopgaveService =
        new ArbejdsopgaveService();

    private readonly DagsprojektService _dagsprojektService =
        new DagsprojektService();

    private readonly KundeService _kundeService =
        new KundeService();

    private readonly List<(OpretArbejdsopgaveDTO Opgave, CheckBox Valgt, Entry Maengde)> _visteArbejdsopgaver = new();

    public OpretDagsprojektPage()
    {
        InitializeComponent();

        StartdatoPicker.Date = DateTime.Today;
        SlutdatoPicker.Date = DateTime.Today;

        HentOgVisKunder();
        HentOgVisArbejdsopgaver();
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
            DisplayAlertAsync(
                "Fejl",
                $"Kunne ikke hente kunder: {ex.Message}",
                "OK");
        }
    }

    private void HentOgVisArbejdsopgaver()
    {
        var arbejdsopgaver = _arbejdsopgaveService.GetAlleArbejdsopgaver();

        ArbejdsopgaverContainer.Children.Clear();
        _visteArbejdsopgaver.Clear();

        foreach (var opgave in arbejdsopgaver)
        {
            var checkBox = new CheckBox();

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
                TextColor = Color.FromArgb("#4A4946")
            };

            checkBox.CheckedChanged += (sender, e) =>
            {
                maengdeEntry.IsEnabled = e.Value;

                if (!e.Value)
                {
                    maengdeEntry.Text = string.Empty;
                }
            };

            var række = new HorizontalStackLayout
            {
                Spacing = 10,
                Children =
                {
                    checkBox,
                    navnLabel,
                    maengdeEntry
                }
            };

            ArbejdsopgaverContainer.Children.Add(række);

            _visteArbejdsopgaver.Add(
                (opgave, checkBox, maengdeEntry));
        }
    }

    private async void OnOpretDagsprojektClicked(object sender, EventArgs e)
    {
        try
        {
            if (KundeListe.SelectedItem is not KundeDTO valgtKunde)
            {
                await DisplayAlertAsync(
                    "Manglende kunde",
                    "Vælg venligst en kunde.",
                    "OK");
                return;
            }

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

            DateTime startdato = StartdatoPicker.Date ?? DateTime.Today;
            DateTime slutdato = SlutdatoPicker.Date ?? DateTime.Today;

            if (slutdato.Date < startdato.Date)
            {
                await DisplayAlertAsync(
                    "Ugyldige datoer",
                    "Slutdato må ikke være før startdato.",
                    "OK");
                return;
            }

            var valgteArbejdsopgaver = new List<ValgtArbejdsopgaveDTO>();

            foreach (var vistOpgave in _visteArbejdsopgaver)
            {
                if (vistOpgave.Valgt.IsChecked)
                {
                    if (!double.TryParse(
                        vistOpgave.Maengde.Text,
                        out double maengde)
                        || !double.IsFinite(maengde)
                        || maengde <= 0)
                    {
                        await DisplayAlertAsync(
                            "Ugyldig mængde",
                            $"Indtast en gyldig mængde for {vistOpgave.Opgave.Name}.",
                            "OK");
                        return;
                    }

                    valgteArbejdsopgaver.Add(new ValgtArbejdsopgaveDTO
                    {
                        ArbejdsopgaveID = vistOpgave.Opgave.ArbejdsopgaveID,
                        Maengde = maengde,
                        ManualTimeHours = null
                    });
                }
            }

            if (valgteArbejdsopgaver.Count == 0)
            {
                await DisplayAlertAsync(
                    "Ingen arbejdsopgaver valgt",
                    "Vælg mindst én arbejdsopgave til projektet.",
                    "OK");
                return;
            }

            var dagsprojekt = new OpretDagsprojektDTO
            {
                KundeID = valgtKunde.Kundenummer,
                Adress = adresse,
                Status = status,
                ExpectedDurationHours = varighed,
                RoofSqrM = tagareal,
                Startdate = startdato.Date,
                Endate = slutdato.Date,
                Arbejdsopgaver = valgteArbejdsopgaver
            };

            _dagsprojektService.OpretNytDagsProjekt(dagsprojekt);

            await DisplayAlertAsync(
                "Projekt oprettet",
                "Dagsprojektet og de valgte arbejdsopgaver er gemt.",
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Fejl ved oprettelse",
                ex.Message,
                "OK");
        }
    }
}
