using Formula1App.Models;
using Formula1App.ModelsExt;
using Formula1App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class ProfileViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1ExtService extService;
        private readonly F1IntService intService;

        private User _user;
        public User _User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }
        private string searchDriver;
        public string SearchDriver
        {
            get => searchDriver;
            set
            {
                searchDriver = value;
                OnPropertyChanged();
                SearchDrivers();
            }
        }
        private string searchConst;
        public string SearchConst
        {
            get => searchConst;
            set
            {
                searchConst = value;
                OnPropertyChanged();
                SearchConstructors();
            }
        }
        private ObservableCollection<MyDriver> drivers;
        public ObservableCollection<MyDriver> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<MyDriver> driversForSearch;
        public ObservableCollection<MyDriver> DriversForSearch
        {
            get => driversForSearch;
            set
            {
                driversForSearch = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Constructor> consts;
        public ObservableCollection<Constructor> Consts
        {
            get => consts;
            set
            {
                consts = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Constructor> constsForSearch;
        public ObservableCollection<Constructor> ConstsForSearch
        {
            get => constsForSearch;
            set
            {
                constsForSearch = value;
                OnPropertyChanged();
            }
        }
        private MyDriver selectedDriver;
        public MyDriver SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                OnPropertyChanged();
            }
        }
        private Constructor selectedConst;
        public Constructor SelectedConst
        {
            get => selectedConst;
            set
            {
                selectedConst = value;
                OnPropertyChanged();
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
        public ICommand DriverSearchCommand { get; set; }
        public ICommand ConstSearchCommand { get; set; }

        public ProfileViewModel(IServiceProvider serviceProvider, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.extService = extService;
            this.intService = intService;
            RefreshCommand = new Command(async () => await Refresh());
            DriverSearchCommand = new Command(async () => await SearchDrivers());
            ConstSearchCommand = new Command(async () => await SearchConstructors());
            _User = ((App)Application.Current).LoggedUser;
            InitData();
        }
        private async void InitData()
        {
            await GetDrivers();
            await GetConstructors();
            await Refresh();
        }
        private async Task GetDrivers()
        {
            Drivers = new(await extService.GetAllDriversAsync());
            DriversForSearch = new(Drivers.OrderBy(d => d.FirstName).ToList());
        }
        private async Task GetConstructors()
        {
            Consts = new(await extService.GetAllConstructorsAsync());
            ConstsForSearch = new(Consts.OrderBy(c => c.OfficialConstructorName).ToList());
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            _User = ((App)Application.Current).LoggedUser;
            SearchDriver = String.Empty;
            SearchConst = String.Empty;
            IsRefreshing = false;
        }
        private async Task SearchDrivers()
        {
            DriversForSearch = new();
            SelectedDriver = null;
            if (!string.IsNullOrEmpty(SearchDriver))
            {
                foreach (MyDriver md in Drivers)
                {
                    if (SearchDriver.Contains(" "))
                    {
                        if (SearchDriver.Length <= md.FullName.Length)
                        {
                            if (md.FullName.Substring(0, SearchDriver.Length).ToLower().Contains(SearchDriver.ToLower()))
                            {
                                if (!DriversForSearch.Contains(md))
                                    DriversForSearch.Add(md);
                            }
                        }
                    }
                    else
                    {
                        if (SearchDriver.Length <= md.FirstName.Length)
                        {
                            if (md.FirstName.Substring(0, SearchDriver.Length).ToLower().Contains(SearchDriver.ToLower()))
                            {
                                if (!DriversForSearch.Contains(md))
                                    DriversForSearch.Add(md);
                            }
                        }
                        if (SearchDriver.Length <= md.LastName.Length)
                        {
                            if (md.LastName.Substring(0, SearchDriver.Length).ToLower().Contains(SearchDriver.ToLower()))
                            {
                                if (!DriversForSearch.Contains(md))
                                    DriversForSearch.Add(md);
                            }
                        }
                    }
                }
            }
            else
            {
                DriversForSearch = Drivers;
            }
            DriversForSearch = new(DriversForSearch.OrderBy(d => d.FirstName).ToList());
        }
        private async Task SearchConstructors()
        {
            ConstsForSearch = new();
            SelectedConst = null;
            if (!string.IsNullOrEmpty(SearchConst))
            {
                foreach (Constructor c in Consts)
                {
                    if (SearchConst.Length <= c.name.Length)
                    {
                        if (c.name.Substring(0, SearchConst.Length).ToLower().Contains(SearchConst.ToLower()))
                        {
                            if (!ConstsForSearch.Contains(c))
                                ConstsForSearch.Add(c);
                        }
                    }
                }
            }
            else
            {
                ConstsForSearch = Consts;
            }
            ConstsForSearch = new(ConstsForSearch.OrderBy(c => c.OfficialConstructorName).ToList());
        }
    }
}
