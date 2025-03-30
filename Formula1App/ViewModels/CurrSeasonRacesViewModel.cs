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
    public class CurrSeasonRacesViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
        private readonly F1ExtService extService;

        private List<Race> races;
        public ObservableCollection<Race> UpcomingRaces { get; set; }
        public ObservableCollection<Race> PastRaces { get; set; }
        private Race selectedRace;
        public Race SelectedRace
        {
            get => selectedRace;
            set
            {
                selectedRace = value;
                OnPropertyChanged();
                //if (selectedRace != null)
                //    NavToRace();
            }
        }
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
        public ICommand RefreshCommand { get; set; }
        public ICommand GoToPrevStandings { get; set; }
        public ICommand GoToRaceCommand { get; set; }

        public CurrSeasonRacesViewModel(IServiceProvider sp, F1IntService intService, F1ExtService extService)
        {
            this.serviceProvider = sp;
            this.intService = intService;
            this.extService = extService;
            races = new();
            PastRaces = new();
            UpcomingRaces = new();
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
            await GetRaces();
            IsRefreshing = false;
        }
        private async Task GetRaces()
        {
            races = await extService.GetCurrRacesAsync();
            int currRound = await extService.GetCurrRound();
            UpcomingRaces.Clear();
            PastRaces.Clear();
            foreach (Race r in races)
            {
                int.TryParse(r.round, out int round);
                if (round <= currRound)
                    PastRaces.Insert(0, r);
                else if (round > currRound)
                    UpcomingRaces.Add(r);
            }
            //foreach (Race r in PastRaces)
            //{
            //    r.Results = await extService.GetRaceResultsAsync("current", r.round);
            //}
        }
    }
}
