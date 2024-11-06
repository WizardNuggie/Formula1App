using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class SignUpViewModel:ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        public ICommand RegisterCommand { get; set; }

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
                        bool IsPasswordOk = IsValidPassword(password);
                        if (!IsPasswordOk)
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
                    OnPropertyChanged(nameof(Email));
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
                        if (char.IsDigit(Name[0]))
                        {
                            NameError = "A name cannot start with a number";
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
        private bool IsValidPassword(string pass)
        {
            int sum = 0;
            foreach (char c in pass)
            {
                sum++;
            }
            return sum >= 4;
        }

        public SignUpViewModel(IServiceProvider sp)
        {
            this.serviceProvider = sp;
        }
    }
}
