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

    public void OpdaterListe()
    {
        collectionViewMedarbejdere.ItemsSource = null;
        collectionViewMedarbejdere.ItemsSource = _medarbejderService.GetAlleMedarbejdere();
    }
}