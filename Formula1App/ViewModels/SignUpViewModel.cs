using Formula1App.Models;
using Formula1App.ModelsExt;
using Formula1App.Services;
using Formula1App.Views;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class SignUpViewModel:ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1ExtService extService;
        private readonly F1IntService intService;
        
        public ICommand RegisterCommand { get; set; }
        public ICommand DriverSearchCommand { get; set; }
        public ICommand ConstSearchCommand { get; set; }
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
        private string username;
        public string Username
        {
            get => username;
            set
            {
               username = value;
               OnPropertyChanged();
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                PassError = "";
                OnPropertyChanged();
                if (string.IsNullOrEmpty(password))
                {
                    PassError = "";
                }
                else
                {
                    if (password != null)
                    {
                        if (!IsValidPassword(password))
                        {
                            PassError = "The password must contain at least 4 characters";
                        }
                    }
                }
            }
        }
        private string passError;
        public string PassError
        {
            get { return passError; }
            set
            {
                passError = value;
                if (!string.IsNullOrEmpty(PassError))
                    IsPassErr = true;
                else
                    IsPassErr = false;
                OnPropertyChanged();
            }
        }
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                EmailError = "";
                OnPropertyChanged();
                IsValidEmail();
                if (string.IsNullOrEmpty(Email))
                    EmailError = "";
            }
        }
        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                if (string.IsNullOrEmpty(EmailError))
                    IsEmailErr = false;
                else
                    IsEmailErr = true;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NameError = "";
                OnPropertyChanged();
                if (!string.IsNullOrEmpty(Name))
                {
                foreach(char c in Name)
                    if (!(c >= 'a' && c <= 'z') && !(c >= 'A' && c <= 'Z') && !(c == ' '))
                    {
                        NameError = "A name can only contain letters";
                        OnPropertyChanged();
                    }
                }
                else
                    NameError = "";
            }
        }
        private string nameError;
        public string NameError
        {
            get { return nameError; }
            set
            {
                nameError = value;
                if (!string.IsNullOrEmpty(NameError))
                    IsNameErr = true;
                else
                    IsNameErr = false;
                OnPropertyChanged();
            }
        }
        private string favDriver;
        public string FavDriver
        {
            get => favDriver;
            set
            {
                favDriver = value;
                OnPropertyChanged();
            }
        }
        private string favConstructor;
        public string FavConstructor
        {
            get => favConstructor;
            set
            {
                favConstructor = value;
                OnPropertyChanged();
            }
        }
        private DateOnly bday;
        public DateOnly Bday
        {
            get => bday;
            set
            {
                bday = value;
                OnPropertyChanged();
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
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }
        private bool isNameErr;
        public bool IsNameErr
        {
            get => isNameErr;
            set
            {
                isNameErr = value;
                OnPropertyChanged();
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }
        private bool isPassErr;
        public bool IsPassErr
        {
            get => isPassErr;
            set
            {
                isPassErr = value;
                OnPropertyChanged();
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }
        private bool isEmailErr;
        public bool IsEmailErr
        {
            get => isEmailErr;
            set
            {
                isEmailErr = value;
                OnPropertyChanged();
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }
        private DateTime maxDate;
        public DateTime MaxDate
        {
            get => maxDate;
            set
            {
                maxDate = value;
                OnPropertyChanged();
            }
        }
        private bool IsValidPassword(string pass)
        {
            return pass.Length >= 4;
        }
        private bool IsValidEmail()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is invalid";
                    return false;
                }
                else
                {
                    EmailError = "";
                    return true;
                }
            }
            return false;
        }
        private List<MyDriver> drivers;
        public List<MyDriver> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }
        private List<MyDriver> driversForSearch;
        public List<MyDriver> DriversForSearch
        {
            get => driversForSearch;
            set
            {
                driversForSearch = value;
                OnPropertyChanged();
            }
        }
        private List<Constructor> constsForSearch;
        public List<Constructor> ConstsForSearch
        {
            get => constsForSearch;
            set
            {
                constsForSearch = value;
                OnPropertyChanged();
            }
        }
        private List<Constructor> constructors;
        public List<Constructor> Constructors
        {
            get => constructors;
            set
            {
                constructors = value;
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
                ((Command)RegisterCommand).ChangeCanExecute();
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
                ((Command)RegisterCommand).ChangeCanExecute();
            }
        }
        public SignUpViewModel(IServiceProvider sp, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = sp;
            this.extService = extService;
            this.intService = intService;
            RegisterCommand = new Command(OnRegister,
                () =>
                !this.IsNameErr &&
                !this.IsPassErr &&
                !this.IsEmailErr &&
                !string.IsNullOrEmpty(this.Username) &&
                this.SelectedDriver != null &&
                this.SelectedConst != null &&
                this.Dob != DateTime.Today);
            Drivers = new();
            DriversForSearch = new();
            DriverSearchCommand = new Command(async () => await SearchDrivers());
            ConstSearchCommand = new Command(async () => await SearchConstructors());
            Constructors = new();
            ConstsForSearch = new();
            MaxDate = DateTime.Today;
            Dob = DateTime.Today;
            GetDrivers();
            GetConstructors();
        }
        private async void OnRegister()
        {
            this.FavDriver = this.SelectedDriver.FullName;
            this.FavConstructor = this.SelectedConst.OfficialConstructorName;
            Bday = DateOnly.FromDateTime(Dob);
            var newUser = new User
            {
                Name = this.Name,
                Email = this.Email,
                Username = this.Username,
                Password = this.Password,
                FavDriver = this.FavDriver,
                FavConstructor = this.FavConstructor,
                Birthday = this.Bday,
                IsAdmin = false,
                Articles = new()
            };
            InServerCall = true;
            ResponseUser newUserResponse = await intService.SignUpAsync(newUser);
            if (newUserResponse != null)
            {
                newUser = newUserResponse.User;
                InServerCall = false;
                if (newUser != null)
                {
                    User u = await intService.LoginAsync(new LoginUser { Username = newUser.Username, Password = newUser.Password });
                    ((App)Application.Current).LoggedUser = u;
                    if (u == null)
                    {
                        string errorMsg = "Sign up succeeded but the login failed.\nTry to login manually";
                        await Application.Current.MainPage.DisplayAlert("Login Failed", errorMsg, "ok");
                        ((App)Application.Current).MainPage = serviceProvider.GetService<SignPage>();
                    }
                    else
                    {
                        AppShell shell = serviceProvider.GetService<AppShell>();
                        ((App)Application.Current).MainPage = shell;
                    }
                }
                else if (newUserResponse.IsExist)
                {
                    string errorMsg = "Username already exists";
                    await Application.Current.MainPage.DisplayAlert("Sign Up Failed", errorMsg, "ok");
                }
            }
            else
            {
                string errorMsg = "Please try again";
                await Application.Current.MainPage.DisplayAlert("Sign Up Failed", errorMsg, "ok");
            }
        }
        private async void GetDrivers()
        {
            Drivers = await extService.GetAllDriversAsync();
            DriversForSearch = Drivers.OrderBy(d => d.FirstName).ToList();
        }
        private async void GetConstructors()
        {
            Constructors = await extService.GetAllConstructorsAsync();
            ConstsForSearch = Constructors.OrderBy(c => c.OfficialConstructorName).ToList();
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
            DriversForSearch = DriversForSearch.OrderBy(d => d.FirstName).ToList();
        }
        private async Task SearchConstructors()
        {
            ConstsForSearch = new();
            SelectedConst = null;
            if (!string.IsNullOrEmpty(SearchConst))
            {
                foreach (Constructor c in Constructors)
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
                ConstsForSearch = Constructors;
            }
            ConstsForSearch = ConstsForSearch.OrderBy(c => c.OfficialConstructorName).ToList();
        }
    }
}
