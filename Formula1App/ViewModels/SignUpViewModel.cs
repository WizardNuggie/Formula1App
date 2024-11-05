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
        public ICommand SignUpCommand { get; set; }

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
                    OnPropertyChanged(nameof(Password));
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
                    OnPropertyChanged(nameof(Name));
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

        public SignUpViewModel() { }
    }
}
