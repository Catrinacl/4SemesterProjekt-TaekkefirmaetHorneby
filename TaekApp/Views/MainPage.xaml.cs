
using TaekApp.Views;
using System.Globalization;
using System.Linq;
using BusinessLogic;

namespace TaekApp.Views
{
    public class CalendarProject
    {
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Color BackgroundColor { get; set; } = Colors.LightBlue;
        public Color TextColor { get; set; } = Colors.Black;
        public Color BorderColor { get; set; } = Colors.Blue;
    }

    public partial class MainPage : ContentPage
    {
        private DateTime _currentDate = DateTime.Now;
        private List<CalendarProject> _projects = new();

        private readonly DagsprojektService _dagsprojektService = new();
        private readonly LangtProjektService _langtProjektService = new();

        public MainPage()
        {
            InitializeComponent();
            LoadSampleProjects();
            BuildCalendar();
        }

        private void OnPrevMonthClicked(object sender, EventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            BuildCalendar();
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            BuildCalendar();
        }

        private void OnTodayClicked(object sender, EventArgs e)
        {
            _currentDate = DateTime.Now;
            BuildCalendar();
        }

        private void LoadSampleProjects()
        {
            var dagsprojekter = _dagsprojektService.GetAlleDagsProjekter();
            var langtidsprojekter = _langtProjektService.GetAlleProjekter();

            var dagsprojektKalender = dagsprojekter.Select(p => new CalendarProject
            {
                Title = $"Dagsprojekt: {p.Adress}",
                StartDate = p.Startdate,
                EndDate = p.Endate,
                BackgroundColor = (Color)Application.Current!.Resources["HornebyOliveGreen"],
                BorderColor = (Color)Application.Current!.Resources["HornebyThatchBrown"],
                TextColor = Color.FromArgb("#F0F0F0")
            });

            var langtidsprojektKalender = langtidsprojekter.Select(p => new CalendarProject
            {
                Title = $"Langtidsprojekt: {p.Adresse}",
                StartDate = p.Startdate,
                EndDate = p.Endate,
                BackgroundColor = Color.FromArgb("#A68867"),
                BorderColor = (Color)Application.Current!.Resources["HornebyThatchBrown"],
                TextColor = Color.FromArgb("#F0F0F0")
            });

            _projects = dagsprojektKalender
                .Concat(langtidsprojektKalender)
                .ToList();
        }

        private void BuildCalendar()
        {
            var cultureInfo = new CultureInfo("da-DK");
            string monthName = _currentDate.ToString("MMMM yyyy", cultureInfo);
            lblCurrentMonth.Text = char.ToUpper(monthName[0]) + monthName.Substring(1);

            CalendarGrid.Children.Clear();
            CalendarGrid.RowDefinitions.Clear();

            for (int r = 0; r < 5; r++)
            {
                CalendarGrid.RowDefinitions.Add(
                    new RowDefinition { Height = GridLength.Star });
            }

            DateTime firstOfMonth = new DateTime(
                _currentDate.Year,
                _currentDate.Month,
                1);

            int dayOfWeekOffset =
                ((int)firstOfMonth.DayOfWeek + 6) % 7;

            DateTime gridStartDate =
                firstOfMonth.AddDays(-dayOfWeekOffset);

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DateTime cellDate =
                        gridStartDate.AddDays(row * 7 + col);

                    bool isCurrentMonth =
                        cellDate.Month == _currentDate.Month;

                    Border dayBorder = new Border
                    {
                        BackgroundColor = isCurrentMonth
                            ? Colors.White
                            : Color.FromArgb("#F5F5F5"),
                        StrokeThickness = 0
                    };

                    Label dayLabel = new Label
                    {
                        Text = cellDate.Day.ToString("D2"),
                        Margin = new Thickness(5),
                        FontSize = 11,
                        FontAttributes = isCurrentMonth
                            ? FontAttributes.Bold
                            : FontAttributes.None,
                        TextColor = isCurrentMonth
                            ? Colors.Black
                            : Colors.Gray
                    };

                    dayBorder.Content = dayLabel;

                    Grid.SetRow(dayBorder, row);
                    Grid.SetColumn(dayBorder, col);

                    CalendarGrid.Children.Add(dayBorder);
                }
            }

            RenderProjectsForCurrentView(gridStartDate);
        }

        private void RenderProjectsForCurrentView(DateTime gridStartDate)
        {
            foreach (var project in _projects)
            {
                for (int row = 0; row < 5; row++)
                {
                    DateTime weekStart = gridStartDate.AddDays(row * 7);
                    DateTime weekEnd = weekStart.AddDays(6);

                    if (project.StartDate <= weekEnd &&
                        project.EndDate >= weekStart)
                    {
                        int startCol = Math.Max(
                            0,
                            (project.StartDate - weekStart).Days);

                        int endCol = Math.Min(
                            6,
                            (project.EndDate - weekStart).Days);

                        int colSpan = endCol - startCol + 1;

                        if (colSpan <= 0)
                            continue;

                        Border projectBorder = new Border
                        {
                            BackgroundColor = project.BackgroundColor,
                            Stroke = project.BorderColor,
                            StrokeThickness = 1,
                            StrokeShape =
                                new Microsoft.Maui.Controls.Shapes.RoundRectangle
                                {
                                    CornerRadius = 4
                                },
                            HeightRequest = 22,
                            VerticalOptions = LayoutOptions.Start,
                            Margin = new Thickness(2, 22, 2, 2)
                        };

                        Label projectLabel = new Label
                        {
                            Text = project.Title,
                            FontSize = 10,
                            TextColor = project.TextColor,
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            Padding = new Thickness(4, 0)
                        };

                        projectBorder.Content = projectLabel;

                        Grid.SetRow(projectBorder, row);
                        Grid.SetColumn(projectBorder, startCol);
                        Grid.SetColumnSpan(projectBorder, colSpan);

                        CalendarGrid.Children.Add(projectBorder);
                    }
                }
            }
        }

        private async void OnOpretProjektClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpretProjektPage());
        }

        private async void OnOpretDagsprojektClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpretDagsprojektPage());
        }

        private async void OnOpretStandardArbejdsopgaveClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpretArbejdsopgavePage());
        }

        private async void OnProjekterOversigtClicked(object sender, EventArgs e)
        {
            KalenderContainer.IsVisible = false;
            IndholdsContainer.IsVisible = true;
            IndholdsContainer.Content = new ProjekterPage();

            btnOpretProjekt.IsVisible = true;
            btnOpretDagsprojekt.IsVisible = true;
            btnOpretArbejdsopgave.IsVisible = true;
            btnOpretMedarbejder.IsVisible = false;
            btnOpretKunde.IsVisible = false;
        }

        private void OnOversigtskalenderClicked(object sender, EventArgs e)
        {
            IndholdsContainer.IsVisible = false;
            IndholdsContainer.Content = null;
            KalenderContainer.IsVisible = true;

            btnOpretProjekt.IsVisible = true;
            btnOpretDagsprojekt.IsVisible = true;
            btnOpretArbejdsopgave.IsVisible = true;
            btnOpretKunde.IsVisible = false;
            btnOpretMedarbejder.IsVisible = false;

            LoadSampleProjects();
            BuildCalendar();
        }

        private void OnMedarbejderoversigtClicked(object sender, EventArgs e)
        {
            KalenderContainer.IsVisible = false;
            IndholdsContainer.IsVisible = true;
            IndholdsContainer.Content = new MedarbejderPage();

            btnOpretProjekt.IsVisible = false;
            btnOpretDagsprojekt.IsVisible = false;
            btnOpretArbejdsopgave.IsVisible = false;
            btnOpretKunde.IsVisible = false;
            btnOpretMedarbejder.IsVisible = true;
        }

        private void OnKundeoversigtClicked(object sender, EventArgs e)
        {
            KalenderContainer.IsVisible = false;
            IndholdsContainer.IsVisible = true;
            IndholdsContainer.Content = new KunderPage();

            btnOpretProjekt.IsVisible = false;
            btnOpretDagsprojekt.IsVisible = false;
            btnOpretArbejdsopgave.IsVisible = false;
            btnOpretMedarbejder.IsVisible = false;
            btnOpretKunde.IsVisible = true;
        }

        private void OnArbejdsopgaveOversigtClicked(object sender, EventArgs e)
        {
            KalenderContainer.IsVisible = false;
            IndholdsContainer.IsVisible = true;
            IndholdsContainer.Content = new ArbejdsopgaverPage();

            btnOpretProjekt.IsVisible = false;
            btnOpretDagsprojekt.IsVisible = false;
            btnOpretArbejdsopgave.IsVisible = false;
            btnOpretKunde.IsVisible = false;
            btnOpretMedarbejder.IsVisible = false;
        }

        private async void OnOpretKundeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpretKundePage());
        }

        private async void OnOpretMedarbejderClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpretMedarbejderPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (IndholdsContainer.Content is MedarbejderPage medarbejderPage)
            {
                medarbejderPage.OpdaterListe();
            }
            else if (IndholdsContainer.Content is KunderPage kunderPage)
            {
                kunderPage.OpdaterListe();
            }
            else if (IndholdsContainer.Content is ProjekterPage projekterPage)
            {
                projekterPage.OpdaterListe();
            }

            if (KalenderContainer.IsVisible)
            {
                LoadSampleProjects();
                BuildCalendar();
            }
        }
    }
}
