using BusinessLogic;
using System.Linq;
using DTO;

namespace TaekApp.Views;

public partial class ProjekterPage : ContentView
{
    private readonly DagsprojektService _dagsprojektService =
    new DagsprojektService();

    public ProjekterPage()
    {
        InitializeComponent();
        TestDataProjekter();
        VisProjekter();
    }

    private void TestDataProjekter()
    {
        if (OpretProjektPage.projektList.Count == 0)
        {
            OpretProjektPage.projektList.Add(new ProjektDTO
            {
                ProjektID = 1001,
                KundeID = 1,
                Adresse = "Vej 1",
                Status = "Igangværende",
                ForventetVarighedTimer = 120,
                KvadratmeterAfTag = 185
            });
            OpretProjektPage.projektList.Add(new ProjektDTO
            {
                ProjektID = 1002,
                KundeID = 2,
                Adresse = "Vej 2",
                Status = "Planlagt",
                ForventetVarighedTimer = 80,
                KvadratmeterAfTag = 130
            });
            OpretProjektPage.projektList.Add(new ProjektDTO
            {
                ProjektID = 1003,
                KundeID = 3,
                Adresse = "Vej 3",
                Status = "Afsluttet",
                ForventetVarighedTimer = 160,
                KvadratmeterAfTag = 240
            });
        }
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

        ProjekterListe.ItemsSource = null;
        ProjekterListe.ItemsSource = OpretProjektPage.projektList;
    }
 
}