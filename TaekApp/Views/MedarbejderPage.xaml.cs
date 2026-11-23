using DataAccess.Model;
using DTO;
using BusinessLogic;

namespace TaekApp.Views;

public partial class MedarbejderPage : ContentView
{
    private readonly MedarbejderService _medarbejderService = new();

    public MedarbejderPage()
    {
        InitializeComponent();
        OpdaterListe();
    }

    public async void OpdaterListe()
    {
        var medarbejdere = await Task.Run(() => _medarbejderService.GetAlleMedarbejdere());
        collectionViewMedarbejdere.ItemsSource = null;
        collectionViewMedarbejdere.ItemsSource = medarbejdere;
    }
}