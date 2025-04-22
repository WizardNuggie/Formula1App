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
        private ObservableCollection<Season> seasonsObs;
        public ObservableCollection<Season> SeasonsObs
            {
                get => seasonsObs;
                set
                {
                    seasonsObs = value;
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
                    InServerCall = true;
                    GetRaces();
                    GetDrivers();
                    GetConsts();
                    GetCats();
                    GetSeasonResults();
                    InServerCall = false;
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
        private ObservableCollection<string> categoriesObs;
        public ObservableCollection<string> CategoriesObs
            {
                get => categoriesObs;
                set
                {
                    categoriesObs = value;
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
                    InServerCall = true;
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
                    InServerCall = false;
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
        private ObservableCollection<Race> racesObs;
        public ObservableCollection<Race> RacesObs
            {
                get => racesObs;
                set
                {
                    racesObs = value;
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
                InServerCall = true;
                if (InRaces && SelectedRace.Circuit.Location.locality == "All")
                {
                    InAllRaces = true;
                    InSpecRace = false;
                    GetRaceResults();
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
                InServerCall = true;
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
        private ObservableCollection<string> raceCatsObs;
        public ObservableCollection<string> RaceCatsObs
            {
                get => raceCatsObs;
                set
                {
                    raceCatsObs = value;
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
                    InServerCall = true;
                    if (InSpecRace)
                    {
                        switch (selectedRaceCat)
                        {
                            case "Race Result":
                                InResult = true;
                                InLaps = false;
                                InPit = false;
                                InGrid = false;
                                InQuali = false;
                                InSprint = false;
                                GetRaceResults();
                                break;
                            case "Fastest Laps":
                                InResult = false;
                                InLaps = true;
                                InPit = false;
                                InGrid = false;
                                InQuali = false;
                                InSprint = false;
                                GetFastestLaps();
                                break;
                            case "Pit Stops":
                                InResult = false;
                                InLaps = false;
                                InPit = true;
                                InGrid = false;
                                InQuali = false;
                                InSprint = false;
                                break;
                            case "Starting Grid":
                                InResult = false;
                                InLaps = false;
                                InPit = false;
                                InGrid = true;
                                InQuali = false;
                                InSprint = false;
                                break;
                            case "Qualifying":
                                InResult = false;
                                InLaps = false;
                                InPit = false;
                                InGrid = false;
                                InQuali = true;
                                InSprint = false;
                                break;
                            case "Sprint":
                                InResult = false;
                                InLaps = false;
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
                        InLaps = false;
                        InPit = false;
                        InGrid = false;
                        InQuali = false;
                        InSprint = false;
                    }
                    InServerCall = false;
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
        private List<MyDriverStandings> orderedDrivers;
        public List<MyDriverStandings> OrderedDrivers
            {
                get => orderedDrivers;
                set
                {
                    orderedDrivers = value;
                    OnPropertyChanged();
                }
            }
        private ObservableCollection<MyDriverStandings> orderedDriversObs;
        public ObservableCollection<MyDriverStandings> OrderedDriversObs
            {
                get => orderedDriversObs;
                set
                {
                    orderedDriversObs = value;
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
                    InServerCall = true;
                    if (InDrivers && SelectedDriver.FirstName == "All")
                        InAllDrivers = true;
                    else
                        InAllDrivers = false;
                    InServerCall = false;
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
        private List<Constructorstanding> orderedConsts;
        public List<Constructorstanding> OrderedConsts
            {
                get => orderedConsts;
                set
                {
                    orderedConsts = value;
                    OnPropertyChanged();
                }
            }
        private ObservableCollection<Constructorstanding> orderedConstsObs;
        public ObservableCollection<Constructorstanding> OrderedConstsObs
            {
                get => orderedConstsObs;
                set
                {
                    orderedConstsObs = value;
                    OnPropertyChanged();
                }
            }
        private Constructorstanding selectedConst;
        public Constructorstanding SelectedConst
            {
                get => selectedConst;
                set
                {
                    selectedConst = value;
                    OnPropertyChanged();
                    InServerCall = true;
                    if (InConsts && SelectedConst.Constructor.name == "All")
                        InAllConsts = true;
                    else
                        InAllConsts = false;
                    InServerCall = false;
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
        private ObservableCollection<Race> seasonResultsObs;
        public ObservableCollection<Race> SeasonResultsObs
            {
                get => seasonResultsObs;
                set
                {
                    seasonResultsObs = value;
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
        private ObservableCollection<Result> raceResultsObs;
        public ObservableCollection<Result> RaceResultsObs
            {
                get => raceResultsObs;
                set
                {
                    raceResultsObs = value;
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
        private bool inLaps;
        public bool InLaps
        {
            get => inLaps;
            set
            {
                inLaps = value;
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
        private bool hasLaps;
        public bool HasLaps
        {
            get => hasLaps;
            set
            {
                hasLaps = value;
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
            Seasons = new();
            Races = new();
            Drivers = new();
            Consts = new();
            SeasonsObs = new();
            RacesObs = new();
            RaceCatsObs = new();
            OrderedDriversObs = new();
            OrderedConstsObs = new();
            SeasonResultsObs = new();
            RaceResultsObs = new();
            InitData();
        }
        private async void InitData()
        {
            await GetSeasons();
            await GetRaces();
            await GetDrivers();
            await GetConsts();
            await GetRaceCats();
        }
        private async Task GetSeasons()
        {
            Seasons = (await extService.GetSeasonsAsync()).OrderByDescending(s => s.season).ToList();
            SeasonsObs = new(Seasons);
            SelectedSeason = Seasons[1];
        }
        private async Task GetRaces()
        {
            Races = await extService.GetRacesByYearAsync(SelectedSeason.season);
            Races.Insert(0, new Race(Races.FirstOrDefault()));
            Races.FirstOrDefault().Circuit.Location.locality = "All";
            RacesObs = new(Races);
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
            OrderedDriversObs = new(OrderedDrivers);
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
            OrderedConstsObs = new(OrderedConsts);
            SelectedConst = OrderedConsts.FirstOrDefault();
        }
        public async Task GetSeasonResults()
        {
            SeasonResults = await extService.GetSeasonResultsAsync(SelectedSeason.season);
            foreach (Race r in SeasonResults)
            {
                Result add = r.Winner;
                r.Results.Clear();
                r.Results.Add(add);
            }
            SeasonResultsObs = new(SeasonResults);
        }
        private async Task GetCats()
        {
            Categories = new();
            Categories.Add("Races");
            Categories.Add("Drivers");
            Categories.Add("Constructors");
            CategoriesObs = new(Categories);
            SelectedCat = Categories.FirstOrDefault();
        }
        private async void GetRaceCatsInit()
        {
            await GetRaceCats();
        }
        private async Task GetRaceCats()
        {
            RaceCats = new();
            RaceCats.Clear();
            RaceCats.Add("Race Result");
            if ((InSpecRace && HasLaps) || InAllRaces)
                RaceCats.Add("Fastest Laps");
            RaceCats.Add("Pit Stops");
            RaceCats.Add("Starting Grid");
            RaceCats.Add("Qualifying");
            if (InSpecRace && SelectedRace.HasSprint)
                RaceCats.Add("Sprint");
            RaceCatsObs = new(RaceCats);
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
            int count = 0;
            foreach (Result r in RaceResults)
            {
                if (r.FastestLap != null)
                {
                    count++;
                }
            }
            RaceResultsObs = new(RaceResults);
            if (count > 0)
                HasLaps = true;
            else
                HasLaps = false;
        }
        private async void GetFastestLaps()
        {
            await GetFastestLapsRace();
        }
        private async Task GetFastestLapsRace()
        {
            List<Result> rr = new(RaceResults);
            foreach (Result r in RaceResults)
            {
                if (r.FastestLap == null)
                {
                    if (rr.Where(x => x.Driver.driverId == r.Driver.driverId).FirstOrDefault() != null)
                        rr.Remove(rr.Where(x => x.Driver.driverId == r.Driver.driverId).FirstOrDefault());
                }
            }
            RaceResults = new(rr);
            RaceResults = RaceResults.OrderBy(r => r.FastestLap.rankInt).ToList();
            int count = 0;
            foreach (Result r in RaceResults)
            {
                if (r.FastestLap != null)
                {
                    count++;
                }
            }
            RaceResultsObs = new(RaceResults);
            if (count > 0)
                HasLaps = true;
            else
                HasLaps = false;
        }
    }
}
