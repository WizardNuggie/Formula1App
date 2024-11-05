﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class LoginViewModel:ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        public ICommand LoginCommand { get; set; }
        public ICommand ToRegisterCommand { get; set; }
        
        private string username;
        public string Username {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value; 
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public LoginViewModel() { }
    }
}
