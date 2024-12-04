using Formula1App.Models;
using Formula1App.Services;
using System;
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
        private readonly F1ExtService extService;
        private readonly F1IntService intService;
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
        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }
        public LoginViewModel(IServiceProvider sp, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = sp;
            this.extService = extService;
            this.intService = intService;
        }

        private async void OnLogin()
        {
            InServerCall = true;
            ErrorMsg = "";
            LoginUser loginInfo = new LoginUser(Username = this.Username, Password = this.Password);
            User u = await this.intService.LoginAsync(loginInfo);
            InServerCall = false;

            ((App)Application.Current).LoggedUser = u;
            if (u == null)
            {
                ErrorMsg = "The username or password is incorrect";
            }
            else
            {
                ErrorMsg = "";
                AppShell shell = serviceProvider.GetService<AppShell>();

            }
        }
    }
}
