namespace TaekApp.Views;

public partial class ProjekterPage : ContentPage
{
    public ProjekterPage()
    {
        InitializeComponent();

        VisProjekter();
    }

    private void OnProjekterClicked(object sender, EventArgs e)
    {
        VisProjekter();
    }

    private void OnDagsprojekterClicked(object sender, EventArgs e)
    {
        ProjekterListe.ItemsSource = new List<string>
        {
            "Reparation af rygning",
            "Eftersyn af tag"
        };
    }

    private void OnOpretDagsprojektClicked(object sender, EventArgs e)
    {
        ProjekterListe.ItemsSource = new List<string>
        {
            "Opret nyt dagsprojekt"
        };
    }

    private void VisProjekter()
    {
        ProjekterListe.ItemsSource = new List<string>
        {
            "Nyt stråtag",
            "Udskiftning af tag",
            "Renovering af lade"
        };
    }
}