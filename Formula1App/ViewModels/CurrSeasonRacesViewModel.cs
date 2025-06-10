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
        private ObservableCollection<Race> upcomingRaces;
        public ObservableCollection<Race> UpcomingRaces
        {
            get => upcomingRaces;
            set
            {
                upcomingRaces = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Race> pastRaces;
        public ObservableCollection<Race> PastRaces
        {
            get => pastRaces;
            set
            {
                pastRaces = value;
                OnPropertyChanged();
            }
        }
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
            GoToPrevStandings = new Command(async () => await NavToPrevStands());
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
                //foreach (Result rs in r.Results)
                //{
                //    rs.Constructor.TeamColor = Color.FromArgb(((App)Application.Current).TeamColors[rs.Constructor.constructorId]);
                //}
                int.TryParse(r.round, out int round);
                if (round <= currRound)
                    PastRaces.Insert(0, r);
                else if (round > currRound)
                    UpcomingRaces.Add(r);
            }
            //List<Race> temp = new List<Race>();
            //foreach (Race r in PastRaces)
            //{
            //    temp.Add(new Race(r));
            //}
            //foreach (Race r in PastRaces)
            //{
            //    Race rc = await extService.GetRaceResultsAsync("current", r.round);
            //    if (temp.Where(rce => rce.round == r.round).FirstOrDefault() != null)
            //    {
            //        temp.Where(rce => rce.round == r.round).FirstOrDefault().Results = rc.Results;
            //    }
            //}
            //PastRaces = new(temp);
            List<Race> temp = PastRaces.Select(r => new Race(r)).ToList(); // Create a copy of the races
            foreach (Race r in temp)
            {
                Race rc = await extService.GetRaceResultsAsync("current", r.round);
                var raceToUpdate = temp.FirstOrDefault(rce => rce.round == r.round);
                if (raceToUpdate != null)
                {
                    raceToUpdate.Results = rc.Results;
                }
            }
            foreach (Race r in temp)
            {
                foreach (Result rs in r.Results)
                {
                    if (((App)Application.Current).TeamColors.ContainsKey(rs.Constructor.constructorId))
                        rs.Constructor.TeamColor = Color.FromArgb(((App)Application.Current).TeamColors[rs.Constructor.constructorId]);
                    else
                        rs.Constructor.TeamColor = Color.FromArgb("#F7F4F1");
                }
            }
            PastRaces = new ObservableCollection<Race>(temp); // Update the ObservableCollection after
        }
        private async Task NavToPrevStands()
        {
            await AppShell.Current.GoToAsync("PrevSeasons");
        }
    }
}
