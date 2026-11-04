using TaekApp.Views;

namespace TaekApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpretProjektClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new newProject());
        }

        private async void OnProjekterOversigtClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjekterPage());
        }




    }
}
