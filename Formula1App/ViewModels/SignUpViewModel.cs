using Formula1App.Models;
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
    public class SignUpViewModel:ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1ExtService service;
        
        public ICommand RegisterCommand { get; set; }
        public ICommand CanRegisterCommand { get; set; }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                    username = value;
                    OnPropertyChanged(nameof(Username));
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
                OnPropertyChanged(nameof(Password));
                if (string.IsNullOrEmpty(password))
                {
                    PassError = "";
                }
                else
                {
                    if (password != null)
                    {
                        if (IsValidPassword(password))
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
                OnPropertyChanged(nameof(PassError));
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
                OnPropertyChanged(nameof(Email));
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
                OnPropertyChanged(nameof(EmailError));
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
                    OnPropertyChanged(nameof(Name));
                    if (!string.IsNullOrEmpty(Name))
                    {
                    foreach(char c in Name)
                        if (!(c >= 'a' && c <= 'z') && !(c >= 'A' && c <= 'Z'))
                        {
                            NameError = "A name can only contain letters";
                            OnPropertyChanged(nameof(Name));
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
                OnPropertyChanged(nameof(NameError));
            }
        }
        private string favDriver;
        public string FavDriver
        {
            get => favDriver;
            set
            {
                    favDriver = value;
                    OnPropertyChanged(nameof(FavDriver));
            }
        }
        private string favConstructor;
        public string Favconstructor
        {
            get => favConstructor;
            set
            {
                    favConstructor = value;
                    OnPropertyChanged(nameof(Favconstructor));
            }
        }
        private DateOnly bday;
        public DateOnly Bday
        {
            get => bday;
            set
            {
                    bday = value;
                    OnPropertyChanged(nameof(Bday));
            }
        }
        private DateTime dob;
        public DateTime Dob
        {
            get => dob;
            set
            {
                dob = value;
                OnPropertyChanged(nameof(Dob));
            }
        }
        private bool isNameErr;
        public bool IsNameErr
        {
            get => isNameErr;
            set
            {
                isNameErr = value;
                OnPropertyChanged(nameof(IsNameErr));
            }
        }
        private bool isPassErr;
        public bool IsPassErr
        {
            get => isPassErr;
            set
            {
                isPassErr = value;
                OnPropertyChanged(nameof(IsPassErr));
            }
        }
        private bool isEmailErr;
        public bool IsEmailErr
        {
            get => isEmailErr;
            set
            {
                isEmailErr = value;
                OnPropertyChanged(nameof(IsEmailErr));
            }
        }
        private DateTime maxDate;
        public DateTime MaxDate
        {
            get => maxDate;
            set
            {
                maxDate = value;
                OnPropertyChanged(nameof(MaxDate));
            }
        }
        private bool IsValidPassword(string pass)
        {
            int sum = 0;
            foreach (char c in pass)
            {
                sum++;
            }
            return sum >= 4;
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
                OnPropertyChanged(nameof(Drivers));
            }
        }
        private List<Constructor> constructors;
        public List<Constructor> Constructors
        {
            get => constructors;
            set
            {
                constructors = value;
                OnPropertyChanged(nameof(Constructors));
            }
        }
        private MyDriver selectedDriver;
        public MyDriver SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                OnPropertyChanged(nameof(SelectedDriver));
            }
        }
        private Constructor selectedConst;
        public Constructor SelectedConst
        {
            get => selectedConst;
            set
            {
                selectedConst = value;
                OnPropertyChanged(nameof(SelectedConst));
            }
        }
        private bool canRegister;
        public bool CanRegister
        {
            get => canRegister;
            set
            {
                canRegister = value;
                if (Username == null || Password == null || Name == null || FavDriver == null || Favconstructor == null || Dob == DateTime.Today || IsNameErr || IsPassErr || !IsValidEmail())
                    canRegister = false;
                else
                    canRegister = true;
                OnPropertyChanged(nameof(CanRegister));
            }
        }
        public SignUpViewModel(IServiceProvider sp, F1ExtService s)
        {
            this.serviceProvider = sp;
            this.service = s;
            //RegisterCommand = new Command(OnRegister);
            Drivers = new();
            Constructors = new();
            MaxDate = DateTime.Today;
            Dob = DateTime.Today;
            GetDrivers();
            GetConstructors();
            
        }
        private async void GetDrivers()
        {
            Drivers = await service.GetCurrDriversAsync();
        }

        private async void GetConstructors()
        {
            Constructors = await service.GetCurrConstructorsAsync();
        }
    }
}
