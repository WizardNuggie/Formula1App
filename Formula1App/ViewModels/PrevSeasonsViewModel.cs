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
using Javax.Microedition.Khronos.Egl;

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
                GetSeasonResults();
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
        private string selectedCat;
        public string SelectedCat
        {
            get => selectedCat;
            set
            {
                selectedCat = value;
                OnPropertyChanged();
                switch (selectedCat)
                {
                    case "Races":
                        InRaces = true;
                        InDrivers = false;
                        InConsts = false;
                        SelectedRace = Races.FirstOrDefault();
                        SelectedDriver = OrderedDrivers.FirstOrDefault();
                        SelectedConst = OrderedConsts.FirstOrDefault();
                        break;
                    case "Drivers":
                        InRaces = false;
                        InDrivers = true;
                        InConsts = false;
                        SelectedDriver = OrderedDrivers.FirstOrDefault();
                        SelectedRace = Races.FirstOrDefault();
                        SelectedConst = OrderedConsts.FirstOrDefault();
                        break;
                    case "Constructors":
                        InRaces = false;
                        InDrivers = false;
                        InConsts = true;
                        SelectedConst = OrderedConsts.FirstOrDefault();
                        SelectedRace = Races.FirstOrDefault();
                        SelectedDriver = OrderedDrivers.FirstOrDefault();
                        break;
                }
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
        private Race selectedRace;
        public Race SelectedRace
        {
            get => selectedRace;
            set
            {
                selectedRace = value;
                OnPropertyChanged();
                if (InRaces && SelectedRace.Circuit.Location.locality == "All")
                {
                    InAllRaces = true;
                    InSpecRace = false;
                }
                else if (InRaces && !(SelectedRace.Circuit.Location.locality == "All"))
                {
                    InAllRaces = false;
                    InSpecRace = true;
                    GetRaceResults();
                }
                else
                {
                    InAllRaces = false;
                    InSpecRace = false;
                }
                GetRaceCatsInit();
            }
        }
        private List<string> raceCats;
        public List<string> RaceCats
        {
            get => raceCats;
            set
            {
                raceCats = value;
                OnPropertyChanged();
            }
        }
        private string selectedRaceCat;
        public string SelectedRaceCat
        {
            get => selectedRaceCat;
            set
            {
                selectedRaceCat = value;
                OnPropertyChanged();
                if (InSpecRace)
                {
                    switch (selectedRaceCat)
                    {
                        case "Race Result":
                            InResult = true;
                            InPit = false;
                            InGrid = false;
                            InQuali = false;
                            InSprint = false;
                            break;
                        case "Pit Stops":
                            InResult = false;
                            InPit = true;
                            InGrid = false;
                            InQuali = false;
                            InSprint = false;
                            break;
                        case "Starting Grid":
                            InResult = false;
                            InPit = false;
                            InGrid = true;
                            InQuali = false;
                            InSprint = false;
                            break;
                        case "Qualifying":
                            InResult = false;
                            InPit = false;
                            InGrid = false;
                            InQuali = true;
                            InSprint = false;
                            break;
                        case "Sprint":
                            InResult = false;
                            InPit = false;
                            InGrid = false;
                            InQuali = false;
                            InSprint = true;
                            break;
                    }
                }
                else
                {
                    InResult = false;
                    InPit = false;
                    InGrid = false;
                    InQuali = false;
                    InSprint = false;
                }
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
        public ObservableCollection<MyDriverStandings> OrderedDrivers { get; set; }
        private MyDriverStandings selectedDriver;
        public MyDriverStandings SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                OnPropertyChanged();
                if (InDrivers && SelectedDriver.FirstName == "All")
                    InAllDrivers = true;
                else
                    InAllDrivers = false;
            }
        }
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
        public ObservableCollection<Constructorstanding> OrderedConsts { get; set; }
        private Constructorstanding selectedConst;
        public Constructorstanding SelectedConst
        {
            get => selectedConst;
            set
            {
                selectedConst = value;
                OnPropertyChanged();
                if (InConsts && SelectedConst.Constructor.name == "All")
                    InAllConsts = true;
                else
                    InAllConsts = false;
            }
        }
        private List<Race> seasonResults;
        public List<Race> SeasonResults
        {
            get => seasonResults;
            set
            {
                seasonResults = value;
                OnPropertyChanged();
            }
        }
        private List<Result> raceResults;
        public List<Result> RaceResults
        {
            get => raceResults;
            set
            {
                raceResults = value;
                OnPropertyChanged();
            }
        }
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
        private bool inAllRaces;
        public bool InAllRaces
        {
            get => inAllRaces;
            set
            {
                inAllRaces = value;
                OnPropertyChanged();
            }
        }
        private bool inSpecRace;
        public bool InSpecRace
        {
            get => inSpecRace;
            set
            {
                inSpecRace = value;
                OnPropertyChanged();
            }
        }
        private bool inAllDrivers;
        public bool InAllDrivers
        {
            get => inAllDrivers;
            set
            {
                inAllDrivers = value;
                OnPropertyChanged();
            }
        }
        public bool InSpecDriver
        {
            get
            {
                if (!InDrivers)
                    return false;
                else
                    return !InAllDrivers;
            }
        }
        private bool inAllConsts;
        public bool InAllConsts
        {
            get => inAllConsts;
            set
            {
                inAllConsts = value;
                OnPropertyChanged();
            }
        }
        public bool InSpecConst
        {
            get
            {
                if (!InConsts)
                    return false;
                else
                    return !InAllConsts;
            }
        }
        private bool inResult;
        public bool InResult
        {
            get => inResult;
            set
            {
                inResult = value;
                OnPropertyChanged();
            }
        }
        private bool inPit;
        public bool InPit
        {
            get => inPit;
            set
            {
                inPit = value;
                OnPropertyChanged();
            }
        }
        private bool inGrid;
        public bool InGrid
        {
            get => inGrid;
            set
            {
                inGrid = value;
                OnPropertyChanged();
            }
        }
        private bool inQuali;
        public bool InQuali
        {
            get => inQuali;
            set
            {
                inQuali = value;
                OnPropertyChanged();
            }
        }
        private bool inSprint;
        public bool InSprint
        {
            get => inSprint;
            set
            {
                inSprint = value;
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
            InitData();
        }
        private async void InitData()
        {
            await GetSeasons();
            await GetRaces();
            await GetDrivers();
            await GetConsts();
            await GetCats();
            await GetRaceCats();
        }
        private async Task GetSeasons()
        {
            Seasons = (await extService.GetSeasonsAsync()).OrderByDescending(s => s.season).ToList();
            SelectedSeason = Seasons[1];
        }
        private async Task GetRaces()
        {
            Races = await extService.GetRacesByYearAsync(SelectedSeason.season);
            Races.Insert(0, new Race(Races.FirstOrDefault()));
            Races.FirstOrDefault().Circuit.Location.locality = "All";
            SelectedRace = Races.FirstOrDefault();
        }
        public async Task GetDrivers()
        {
            Drivers = await extService.GetDriversStandingsByYearAsync(SelectedSeason.season);
            OrderedDrivers = new(Drivers.OrderBy(d => d.LastName).ToList());
            OrderedDrivers.Insert(0, new MyDriverStandings()
            {
                FirstName = "All"
            });
            SelectedDriver = OrderedDrivers.FirstOrDefault();
        }
        public async Task GetConsts()
        {
            Consts = await extService.GetConstructorsStandingsByYearAsync(SelectedSeason.season);
            OrderedConsts = new(Consts.OrderBy(c => c.Constructor.OfficialConstructorName).ToList());
            OrderedConsts.Insert(0, new Constructorstanding()
            {
                Constructor = new Constructor()
                {
                    name = "All",
                    constructorId = "All"
                }
            });
            SelectedConst = OrderedConsts.FirstOrDefault();
        }
        public async Task GetSeasonResults()
        {
            InServerCall = true;
            SeasonResults = await extService.GetSeasonResultsAsync(SelectedSeason.season);
            foreach (Race r in SeasonResults)
            {
                Result add = r.Winner;
                r.Results.Clear();
                r.Results.Add(add);
            }
            InServerCall = false;
        }
        private async Task GetCats()
        {
            Categories.Add("Races");
            Categories.Add("Drivers");
            Categories.Add("Constructors");
            SelectedCat = Categories.FirstOrDefault();
        }
        private async void GetRaceCatsInit()
        {
            await GetRaceCats();
        }
        private async Task GetRaceCats()
        {
            RaceCats = new();
            RaceCats.Add("Race Result");
            RaceCats.Add("Pit Stops");
            RaceCats.Add("Starting Grid");
            RaceCats.Add("Qualifying");
            if (InSpecRace && SelectedRace.HasSprint)
                RaceCats.Add("Sprint");
            SelectedRaceCat = RaceCats.FirstOrDefault();
        }
        private async void GetRaceResults()
        {
            Race rs = await extService.GetRaceResultsAsync(SelectedSeason.season, SelectedRace.round);
            foreach (Result res in rs.Results)
            {
                if (res.OffStatus.Contains("Lap"))
                {
                    int.TryParse(res.laps, out int l);
                    int.TryParse(rs.Winner.laps, out int l2);
                    if (l2 > l)
                    {
                        if (l2 - l == 1)
                            res.status = $"+{l2-l} Lap";
                        else
                            res.status = $"+{l2 - l} Laps";
                    }
                }
            }
            RaceResults = rs.Results;
        }
    }
}
