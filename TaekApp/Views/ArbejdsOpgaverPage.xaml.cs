
using System.Diagnostics;
using BusinessLogic;

namespace TaekApp.Views;

public partial class ArbejdsopgaverPage : ContentView
{
    private readonly ArbejdsopgaveService _service =
        new ArbejdsopgaveService();

    public ArbejdsopgaverPage()
    {
        InitializeComponent();
        HentArbejdsopgaver();
    }

    public void HentArbejdsopgaver()
    {
        var arbejdsopgaver = _service.GetAlleArbejdsopgaver();

        Debug.WriteLine(
            $"Antal arbejdsopgaver hentet: {arbejdsopgaver.Count}");

        ArbejdsopgaverListe.ItemsSource = arbejdsopgaver;
    }
}
