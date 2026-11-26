
using BusinessLogic;
using System.Linq;
using DTO;

namespace TaekApp.Views;

public partial class ProjekterPage : ContentView
{
    private readonly DagsprojektService _dagsprojektService = new();
    private readonly LangtProjektService _langtProjektService = new();

    public ProjekterPage()
    {
        InitializeComponent();
        VisProjekter();
    }

    public void OpdaterListe()
    {
        VisProjekter();
    }

    private void OnProjekterClicked(object sender, EventArgs e)
    {
        VisProjekter();
    }

    private void OnDagsprojekterClicked(object sender, EventArgs e)
    {
        btnProjekter.BackgroundColor = Color.FromArgb("#A68867");
        btnDagsprojekter.BackgroundColor = Color.FromArgb("#617300");

        var dagsprojekter = _dagsprojektService.GetAlleDagsProjekter();

        var visteProjekter = dagsprojekter.Select(p => new ProjektDTO
        {
            ProjektID = p.ProjektID,
            Adresse = p.Adress,
            Status = p.Status,
            ForventetVarighedTimer = p.ExpectedDurationHours,
            KvadratmeterAfTag = p.RoofSqrM
        }).ToList();

        ProjekterListe.ItemsSource = null;
        ProjekterListe.ItemsSource = visteProjekter;
    }

    private void VisProjekter()
    {
        btnProjekter.BackgroundColor = Color.FromArgb("#617300");
        btnDagsprojekter.BackgroundColor = Color.FromArgb("#A68867");

        var langtidsprojekter = _langtProjektService.GetAlleProjekter();

        var visteProjekter = langtidsprojekter.Select(p => new ProjektDTO
        {
            ProjektID = p.ProjektID,
            Adresse = p.Adresse,
            Status = p.Status,
            ForventetVarighedTimer = p.ForventetVarighedTimer,
            KvadratmeterAfTag = p.KvadratmeterAfTag
        }).ToList();

        ProjekterListe.ItemsSource = null;
        ProjekterListe.ItemsSource = visteProjekter;
    }
}
