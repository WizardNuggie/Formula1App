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
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
                CheckUserDiff();
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
                CheckUserDiff();
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
                CheckUserDiff();
                ShowEmailErr = !(IsValidEmail());
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                CheckUserDiff();
                ShowPassErr = !(IsValidPassword());
            }
        }
        private DateTime dob;
        public DateTime Dob
        {
            get => dob;
            set
            {
                dob = value;
                OnPropertyChanged();
                CheckUserDiff();
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
                if (selectedDriver != null && (SelectedDriverHolder == null || selectedDriver.DriverId != SelectedDriverHolder.DriverId))
                {
                    SelectedDriverHolder = selectedDriver;
                }
                CheckUserDiff();
            }
        }
        private MyDriver selectedDriverHolder;
        public MyDriver SelectedDriverHolder
        {
            get => selectedDriverHolder;
            set
            {
                selectedDriverHolder = value;
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
                if (selectedConst != null && (SelectedConstHolder == null || selectedConst.constructorId != SelectedConstHolder.constructorId))
                {
                    SelectedConstHolder = selectedConst;
                }
                CheckUserDiff();
            }
        }
        private Constructor selectedConstHolder;
        public Constructor SelectedConstHolder
        {
            get => selectedConstHolder;
            set
            {
                selectedConstHolder = value;
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
        private bool showPass;
        public bool ShowPass
        {
            get => showPass;
            set
            {
                showPass = value;
                OnPropertyChanged();
            }
        }
        private bool isUserDiff;
        public bool IsUserDiff
        {
            get => isUserDiff;
            set
            {
                isUserDiff = value;
                OnPropertyChanged();
                ((Command)CancelChangesCommand).ChangeCanExecute();
                ((Command)SubmitChangesCommand).ChangeCanExecute();
            }
        }
        private bool showEmailErr;
        public bool ShowEmailErr
        {
            get => showEmailErr;
            set
            {
                showEmailErr = value;
                OnPropertyChanged();
                ((Command)CancelChangesCommand).ChangeCanExecute();
                ((Command)SubmitChangesCommand).ChangeCanExecute();
            }
        }
        private bool showPassErr;
        public bool ShowPassErr
        {
            get => showPassErr;
            set
            {
                showPassErr = value;
                OnPropertyChanged();
                ((Command)CancelChangesCommand).ChangeCanExecute();
                ((Command)SubmitChangesCommand).ChangeCanExecute();
            }
        }
        private string emailErr;
        public string EmailErr
        {
            get => emailErr;
            set
            {
                emailErr = value;
                OnPropertyChanged();
            }
        }
        private string passErr;
        public string PassErr
        {
            get => passErr;
            set
            {
                passErr = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; set; }
        public ICommand DriverSearchCommand { get; set; }
        public ICommand ConstSearchCommand { get; set; }
        public ICommand SubmitChangesCommand { get; set; }
        public ICommand CancelChangesCommand { get; set; }
        public ICommand ShowPassCommand { get; set; }

        public ProfileViewModel(IServiceProvider serviceProvider, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.extService = extService;
            this.intService = intService;
            RefreshCommand = new Command(async () => await Refresh());
            DriverSearchCommand = new Command(async () => await SearchDrivers());
            ConstSearchCommand = new Command(async () => await SearchConstructors());
            SubmitChangesCommand = new Command(async () => await SubmitChanges(), () => IsUserDiff && !ShowEmailErr && !ShowPassErr);
            CancelChangesCommand = new Command(async () => await Refresh(), () => IsUserDiff && !ShowEmailErr && !ShowPassErr);
            ShowPassCommand = new Command(ShowPassword);
            _User = new(((App)Application.Current).LoggedUser);
            ShowPass = true;
            InitData();
        }

        private async void InitData()
        {
            Username = _User.Username;
            Name = _User.Name;
            Email = _User.Email;
            Password = _User.Password;
            Dob = new(_User.Birthday, new TimeOnly());
            await GetDrivers();
            await GetConstructors();
        }
        private async Task GetDrivers()
        {
            Drivers = new(await extService.GetAllDriversAsync());
            DriversForSearch = new(Drivers.OrderBy(d => d.FirstName).ToList());
            MyDriver temp = Drivers.Where(d => (d.FullName.ToLower() == _User.FavDriver.ToLower())).FirstOrDefault();
            if (temp != null)
            {
                DriversForSearch.Remove(Drivers.Where(d => (d.FullName.ToLower() == _User.FavDriver.ToLower())).FirstOrDefault());
                DriversForSearch.Insert(0, temp);
            }
            SelectedDriver = temp;
        }
        private async Task GetConstructors()
        {
            Consts = new(await extService.GetAllConstructorsAsync());
            ConstsForSearch = new(Consts.OrderBy(c => c.OfficialConstructorName).ToList());
            Constructor temp = Consts.Where(c => c.OfficialConstructorName.ToLower() == _User.FavConstructor.ToLower()).FirstOrDefault();
            if (temp != null)
            {
                ConstsForSearch.Remove(Consts.Where(c => c.OfficialConstructorName.ToLower() == _User.FavConstructor.ToLower()).FirstOrDefault());
                ConstsForSearch.Insert(0, temp);
            }
            SelectedConst = temp;
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            _User = new(((App)Application.Current).LoggedUser);
            Username = _User.Username;
            Name = _User.Name;
            Email = _User.Email;
            Password = _User.Password;
            Dob = new(_User.Birthday, new TimeOnly());
            SearchDriver = String.Empty;
            SearchConst = String.Empty;
            #region Reset Selected Driver
            MyDriver tempD = Drivers.Where(d => (d.FullName.ToLower() == _User.FavDriver.ToLower())).FirstOrDefault();
            if (tempD != null)
            {
                DriversForSearch.Remove(Drivers.Where(d => (d.FullName.ToLower() == _User.FavDriver.ToLower())).FirstOrDefault());
                DriversForSearch.Insert(0, tempD);
            }
            SelectedDriver = tempD;
            #endregion
            #region Reset Selected Cosnt
            Constructor tempC = Consts.Where(c => c.OfficialConstructorName.ToLower() == _User.FavConstructor.ToLower()).FirstOrDefault();
            if (tempC != null)
            {
                ConstsForSearch.Remove(Consts.Where(c => c.OfficialConstructorName.ToLower() == _User.FavConstructor.ToLower()).FirstOrDefault());
                ConstsForSearch.Insert(0, tempC);
            }
            SelectedConst = tempC;
            #endregion
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
            if (DriversForSearch.Where(d => d.DriverId == SelectedDriverHolder.DriverId).FirstOrDefault() != null)
            {
                SelectedDriver = SelectedDriverHolder;
                DriversForSearch.Remove(DriversForSearch.Where(d => d.DriverId == SelectedDriver.DriverId).FirstOrDefault());
                DriversForSearch.Insert(0, SelectedDriver);
            }
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
            if (ConstsForSearch.Where(c => c.constructorId == SelectedConstHolder.constructorId).FirstOrDefault() != null)
            {
                SelectedConst = SelectedConstHolder;
                ConstsForSearch.Remove(ConstsForSearch.Where(c => c.constructorId == SelectedConst.constructorId).FirstOrDefault());
                ConstsForSearch.Insert(0, SelectedConst);
            }
        }
        private async Task SubmitChanges()
        {
            _User.Name = Name;
            _User.Username = Username;
            _User.Email = Email;
            _User.Password = Password;
            _User.Birthday = DateOnly.FromDateTime(Dob);
            _User.FavDriver = SelectedDriverHolder.FullName;
            _User.FavConstructor = SelectedConstHolder.OfficialConstructorName;
            ResponseUser u = await intService.EditUserDetails(_User);
            if (u != null && !u.IsExist)
            {
                AppShell.Current.DisplayAlert("Information Changed Successfully", "Your information were changed successfully.", "OK");
                ((App)Application.Current).LoggedUser = new(u.User);
                _User = new(((App)Application.Current).LoggedUser);
                await Refresh();
            }
            else if (u != null && u.IsExist)
            {
                AppShell.Current.DisplayAlert("Username Already Exists", "The username you entered is already in use by another account.\nPlease try a different username.", "OK");
            }
            else
            {
                AppShell.Current.DisplayAlert("Something Went Wrong", "Something went wrong while updating your information.\nPlease try again later.", "OK");
            }
        }
        private void ShowPassword()
        {
            ShowPass = !ShowPass;
        }
        private async void CheckUserDiff()
        {
            User u = ((App)Application.Current).LoggedUser;
            if (Email != u.Email ||
                Username != u.Username ||
                Name != u.Name ||
                Password != u.Password ||
                DateOnly.FromDateTime(Dob) != u.Birthday ||
                (SelectedDriverHolder != null && SelectedDriverHolder.FullName.ToLower() != u.FavDriver.ToLower()) ||
                (SelectedConstHolder != null && SelectedConstHolder.OfficialConstructorName.ToLower() != u.FavConstructor.ToLower()))
            {
                IsUserDiff = true;
            }
            else
                IsUserDiff = false;
        }
        private bool IsValidPassword()
        {
            if (Password.Length < 4)
            {
                PassErr = "Password must contain 4 or more characters";
                return false;
            }
            else
            {
                PassErr = "";
                return true;
            }
        }
        private bool IsValidEmail()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailErr = "Email is invalid";
                    return false;
                }
                else
                {
                    EmailErr = "";
                    return true;
                }
            }
            return false;
        }
    }
}
