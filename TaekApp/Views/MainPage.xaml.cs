using TaekApp.Views;
using System.Globalization;

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

        public MainPage()
        {
            InitializeComponent();
            LoadSampleProjects();
            BuildCalendar();
        }

        //Pilene til at klikke en måned tilbage
        private void OnPrevMonthClicked(object sender, EventArgs e)
        {
            _currentDate = _currentDate.AddMonths(-1);
            BuildCalendar();
        }

        //Pilene til at klikke en måned frem
        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            _currentDate = _currentDate.AddMonths(1);
            BuildCalendar();
        }

        //Når man klikker på "I dag"
        private void OnTodayClicked(object sender, EventArgs e)
        {
            _currentDate = DateTime.Now;
            BuildCalendar();
        }

        //Vores test projekter, som vi kan se i kalenderen
        private void LoadSampleProjects()
        {
            _projects = new List<CalendarProject>
            {
                new CalendarProject
                {
                    Title = "Stråtag - Århus Vej 1",
                    StartDate = new DateTime(2026, 10, 27),
                    EndDate = new DateTime(2026, 10, 30),
                    BackgroundColor = Color.FromArgb("#FFE0B2"),
                    BorderColor = Color.FromArgb("#FB8C00"),
                    TextColor = Color.FromArgb("#E65100")
                },
                new CalendarProject
                {
                    Title = "Stråtag - Terpvej 68",
                    StartDate = new DateTime(2026, 10, 29),
                    EndDate = new DateTime(2026, 11, 1),
                    BackgroundColor = Color.FromArgb("#FFF9C4"),
                    BorderColor = Color.FromArgb("#FBC02D"),
                    TextColor = Color.FromArgb("#F57F17")
                },
                new CalendarProject
                {
                    Title = "Reparation af mønning",
                    StartDate = new DateTime(2026, 11, 11),
                    EndDate = new DateTime(2026, 11, 13),
                    BackgroundColor = Color.FromArgb("#E1BEE7"),
                    BorderColor = Color.FromArgb("#8E24AA"),
                    TextColor = Color.FromArgb("#4A148C")
                },
                new CalendarProject
                {
                    Title = "Nyt stråtag - Randersvej 14",
                    StartDate = new DateTime(2026, 12, 7),
                    EndDate = new DateTime(2026, 12, 11),
                    BackgroundColor = Color.FromArgb("#C8E6C9"),
                    BorderColor = Color.FromArgb("#4CAF50"),
                    TextColor = Color.FromArgb("#1B5E20")
                },
                new CalendarProject
                {
                    Title = "Eftersyn & Reparation",
                    StartDate = new DateTime(2027, 1, 14),
                    EndDate = new DateTime(2027, 1, 18),
                    BackgroundColor = Color.FromArgb("#BBDEFB"),
                    BorderColor = Color.FromArgb("#1976D2"),
                    TextColor = Color.FromArgb("#0D47A1")
                }
            };
        }

        //Den her metode bygger og opdaterer kalenderen, når man fx skifter måned eller klikker på "I dag"
        //Den gør kalenderen dynmaisk :)
        private void BuildCalendar()
        {
            //Opdatere overskriften
            var cultureInfo = new CultureInfo("da-DK");
            string monthName = _currentDate.ToString("MMMM yyyy", cultureInfo);
            lblCurrentMonth.Text = char.ToUpper(monthName[0]) + monthName.Substring(1);

            //Rydder det gamle kalender gitter
            CalendarGrid.Children.Clear();
            CalendarGrid.RowDefinitions.Clear();

            // Tilføjer 5 rækker til ugerne
            for (int r = 0; r < 5; r++)
            {
                CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            }

            // Her finder den startdatoen for måneden i kalenderen (mandag)
            DateTime firstOfMonth = new DateTime(_currentDate.Year, _currentDate.Month, 1);
            int dayOfWeekOffset = ((int)firstOfMonth.DayOfWeek + 6) % 7; // Mandag = 0, Søndag = 6
            DateTime gridStartDate = firstOfMonth.AddDays(-dayOfWeekOffset);

            // Datofelterne (5 rækker x 7 kolonner)
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DateTime cellDate = gridStartDate.AddDays(row * 7 + col);
                    bool isCurrentMonth = cellDate.Month == _currentDate.Month;

                    Border dayBorder = new Border
                    {
                        BackgroundColor = isCurrentMonth ? Colors.White : Color.FromArgb("#F5F5F5"),
                        StrokeThickness = 0
                    };

                    Label dayLabel = new Label
                    {
                        Text = cellDate.Day.ToString("D2"),
                        Margin = new Thickness(5),
                        FontSize = 11,
                        FontAttributes = isCurrentMonth ? FontAttributes.Bold : FontAttributes.None,
                        TextColor = isCurrentMonth ? Colors.Black : Colors.Gray
                    };

                    dayBorder.Content = dayLabel;
                    Grid.SetRow(dayBorder, row);
                    Grid.SetColumn(dayBorder, col);
                    CalendarGrid.Children.Add(dayBorder);
                }
            }

            // Indsætter projekter der falder i denne månedsvisning
            RenderProjectsForCurrentView(gridStartDate);
        }

        private void RenderProjectsForCurrentView(DateTime gridStartDate)
        {
            foreach (var project in _projects)
            {
                // Her tjekker man hver ugerække (0-4) for om projektet overlapper
                for (int row = 0; row < 5; row++)
                {
                    DateTime weekStart = gridStartDate.AddDays(row * 7);
                    DateTime weekEnd = weekStart.AddDays(6);

                    // Hvis projektet overlapper med den pågældende uge
                    if (project.StartDate <= weekEnd && project.EndDate >= weekStart)
                    {
                        int startCol = Math.Max(0, (project.StartDate - weekStart).Days);
                        int endCol = Math.Min(6, (project.EndDate - weekStart).Days);
                        int colSpan = endCol - startCol + 1;

                        Border projectBorder = new Border
                        {
                            BackgroundColor = project.BackgroundColor,
                            Stroke = project.BorderColor,
                            StrokeThickness = 1,
                            StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 4 },
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
            await Navigation.PushAsync(new newProject());
        }

        private async void OnProjekterOversigtClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjekterPage());
        }

    }
}
