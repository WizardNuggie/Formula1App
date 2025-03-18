using Formula1App.Services;
using Formula1App.Models;
using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class DriverStandingsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
        private readonly F1ExtService extService;

        private List<MyDriverStandings> standings;
        public ObservableCollection<MyDriverStandings> Standings { get; private set; }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        private MyDriverStandings selectedDriver;
        public MyDriverStandings SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                OnPropertyChanged();
                if (selectedDriver != null)
                    NavToDriver();
            }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand GoToPrevStandings { get; set; }

        public DriverStandingsViewModel(IServiceProvider sp, F1IntService intService, F1ExtService extService)
        {
            this.serviceProvider = sp;
            this.intService = intService;
            this.extService = extService;
            standings = new();
            Standings = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            InitData();
        }
        private async void InitData()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetStandings();
            IsRefreshing = false;
        }
        private async Task GetStandings()
        {
            standings = await extService.GetCurrDriversStandingsAsync();
            Standings.Clear();
            foreach (MyDriverStandings mds in standings)
            {
                mds.TeamColor = Color.FromArgb(((App)Application.Current).TeamColors[mds.Constructors.Last().constructorId]);
                if (mds.PositionText == "-")
                {
                    mds.PositionText = "NC";
                    mds.BackColor = Color.FromArgb("#FFFFFF");
                    mds.TextColor = Color.FromArgb("000000");
                    mds.ArrowColor = Color.FromArgb("E11900");
                }
                else if (mds.PositionText == "1")
                {
                    mds.BackColor = Color.FromArgb("#383840");
                    mds.TextColor = Color.FromArgb("FFFFFF");
                    mds.ArrowColor = Color.FromArgb("FFFFFF");
                }
                else
                {
                    mds.BackColor = Color.FromArgb("#FFFFFF");
                    mds.TextColor = Color.FromArgb("000000");
                    mds.ArrowColor = Color.FromArgb("E11900");
                }
                Standings.Add(mds);
            }
        }
        private async Task NavToDriver()
        {
            Dictionary<string, object> data = new();
            data.Add("Driver", SelectedDriver);
            await AppShell.Current.GoToAsync("Driver", data);
            SelectedDriver = null;
        }
    }
}
