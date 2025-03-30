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
    public class PrevSeasonsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
        private readonly F1ExtService extService;

        private List<Season> seasons;
        public List<Season> Seasons
        {
            get => seasons;
            set
            {
                seasons = value;
                OnPropertyChanged();
            }
        }
        private Season selectedSeason;
        public Season SelectedSeason
        {
            get => selectedSeason;
            set
            {
                selectedSeason = value;
                OnPropertyChanged();
            }
        }
        private List<string> categories;
        public List<string> Categories
        {
            get => categories;
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }
        private List<Race> races;
        public List<Race> Races
        {
            get => races;
            set
            {
                races = value;
                OnPropertyChanged();
            }
        }
        private List<MyDriverStandings> drivers;
        public List<MyDriverStandings> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MyDriverStandings> OrderedDrivers { get; set; }//order by last name
        private List<Constructorstanding> consts;
        public List<Constructorstanding> Consts
        {
            get => consts;
            set
            {
                consts = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Constructorstanding> OrderedConsts { get; set; }//order by offname
        private bool inRaces;
        public bool InRaces
        {
            get => inRaces;
            set
            {
                inRaces = value;
                OnPropertyChanged();
            }
        }
        private bool inDrivers;
        public bool InDrivers
        {
            get => inDrivers;
            set
            {
                inDrivers = value;
                OnPropertyChanged();
            }
        }
        private bool inConsts;
        public bool InConsts
        {
            get => inConsts;
            set
            {
                inConsts = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand RacesAllCommand { get; set; }
        public ICommand DriversAllCommand { get; set; }
        public ICommand ConstsAllCommand { get; set; }

        public PrevSeasonsViewModel(IServiceProvider sp, F1IntService intService, F1ExtService extService)
        {
            this.serviceProvider = sp;
            this.intService = intService;
            this.extService = extService;
            Categories = new();
            Seasons = new();
            Races = new();
            Drivers = new();
            Consts = new();
            InRaces = true;
            InDrivers = false;
            InConsts = false;
            InitData();
        }
        private async void InitData()
        {
            await GetCats();
            await GetSeasons();
            await GetRaces();
            await GetDrivers();
            await getConsts();
        }
        private async Task GetCats()
        {
            Categories.Add("Races");
            Categories.Add("Drivers");
            Categories.Add("Constructors");
        }
        private async Task GetSeasons()
        {
            Seasons = (await extService.GetSeasonsAsync()).OrderByDescending(s => s.season).ToList();
            SelectedSeason = Seasons[1];
        }
        private async Task GetRaces()
        {
            Races = await extService.GetRacesByYearAsync(SelectedSeason.season);
        }
        public async Task GetDrivers()
        {
            Drivers = await extService.GetDriversStandingsByYearAsync(SelectedSeason.season);
            OrderedDrivers = new(Drivers.OrderBy(d => d.LastName).ToList());
        }
        public async Task getConsts()
        {
            Consts = await extService.GetConstructorsStandingsByYearAsync(SelectedSeason.season);
            OrderedConsts = new(Consts.OrderBy(c => c.Constructor.OfficialConstructorName).ToList());
        }
    }
}
