namespace TaekApp.Views;

public partial class ProjekterPage : ContentPage
{
	public ProjekterPage()
	{
		InitializeComponent();

        ProjekterListe.ItemsSource = new List<string>
{
    "Nyt stråtag",
    "Udskiftning af tag",
    "Renovering af lade"
};
    }
}