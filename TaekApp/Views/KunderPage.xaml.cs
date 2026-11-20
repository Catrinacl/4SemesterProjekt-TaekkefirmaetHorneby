using BusinessLogic;
using DTO;

namespace TaekApp.Views;

public partial class KunderPage : ContentView
{
    private readonly KundeService _kundeService = new();

    public KunderPage()
    {
        InitializeComponent();
        OpdaterListe();
    }

    public void OpdaterListe()  
    {
        collectionViewKunder.ItemsSource =
            _kundeService.GetAlleKunder();
    }
}