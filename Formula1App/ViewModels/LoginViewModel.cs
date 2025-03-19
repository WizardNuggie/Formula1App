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
        
        private string username;
        public string Username {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value; 
                    OnPropertyChanged();
                    ((Command)LoginCommand).ChangeCanExecute();
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
                    OnPropertyChanged();
                    ((Command)LoginCommand).ChangeCanExecute();
                }
            }
        }

        public LoginViewModel(IServiceProvider sp, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = sp;
            this.extService = extService;
            this.intService = intService;
            LoginCommand = new Command(OnLogin,
                () =>
                !string.IsNullOrEmpty(Username) &&
                !string.IsNullOrEmpty(Password));
        }

        private async void OnLogin()
        {
            InServerCall = true;
            LoginUser loginInfo = new LoginUser(Username = this.Username, Password = this.Password);
            User u = await this.intService.LoginAsync(loginInfo);
            InServerCall = false;

            ((App)Application.Current).LoggedUser = u;
            if (u == null)
            {
                string errorMsg = "The username or password is incorrect";
                await Application.Current.MainPage.DisplayAlert("Login Failed", errorMsg, "ok");
            }
            else
            {
                AppShell shell = serviceProvider.GetService<AppShell>();
                ((App)Application.Current).MainPage = shell;
            }
        }
    }
}
