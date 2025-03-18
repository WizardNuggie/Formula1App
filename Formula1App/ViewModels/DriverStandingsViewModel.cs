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
                mds.TeamColor = ((App)Application.Current).TeamColors[mds.Constructors.Last().constructorId];
                if (mds.PositionText == "-")
                {
                    mds.PositionText = "NC";
                    mds.BackColor = "#FFFFFF";
                    mds.TextColor = "000000";
                    mds.ArrowColor = "E11900";
                }
                else if (mds.PositionText == "1")
                {
                    mds.BackColor = "#383840";
                    mds.TextColor = "FFFFFF";
                    mds.ArrowColor = "FFFFFF";
                }
                else
                {
                    mds.BackColor = "#FFFFFF";
                    mds.TextColor = "000000";
                    mds.ArrowColor = "E11900";
                }
                Standings.Add(mds);
            }
        }
    }
}
