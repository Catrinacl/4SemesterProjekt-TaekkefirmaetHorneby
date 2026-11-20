using DTO;

namespace TaekApp.Views;

public partial class ProjekterPage : ContentView
{
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

        ProjekterListe.ItemsSource = new List<ProjektDTO>
        {
            new ProjektDTO
            {
                ProjektID = 2001,
                Adresse = "Vej 4 (Reparation af rygning)",
                Status = "Planlagt",
                ForventetVarighedTimer = 8,
                KvadratmeterAfTag = 25
            },
            new ProjektDTO
            {
                ProjektID = 2002,
                Adresse = "Vej 5 (Eftersyn af tag)",
                Status = "Fremtidigt projekt",
                ForventetVarighedTimer = 4,
                KvadratmeterAfTag = 110
            }
        };
    }




    private void VisProjekter()
    {
        btnProjekter.BackgroundColor = Color.FromArgb("#617300");
        btnDagsprojekter.BackgroundColor = Color.FromArgb("#A68867");

        ProjekterListe.ItemsSource = null;
        ProjekterListe.ItemsSource = OpretProjektPage.projektList;
    }
 
}