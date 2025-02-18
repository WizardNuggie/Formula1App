using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Views;

namespace Formula1App.ViewModels
{
    public class AppShellViewModel : ViewModelsBase
    {
        private IServiceProvider serviceProvider;
        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                if (((App)Application.Current).LoggedUser != null)
                {
                    if (((App)Application.Current).LoggedUser.IsAdmin)
                        isAdmin = true;
                    else
                        isAdmin = false;
                }
                else
                    isAdmin = false;
            }
        }
        private bool isWriter;
        public bool IsWriter
        {
            get => isWriter;
            set
            {
                if (((App)Application.Current).LoggedUser != null)
                {
                    if (((App)Application.Current).LoggedUser.UserTypeId == 1)
                        isWriter = true;
                    else
                        isWriter = false;
                }
                else
                    isWriter = false;
            }
        }
        public Command LogoutCommand
        {
            get
            {
                return new Command(Logout);
            }
        }
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            if (((App)Application.Current).LoggedUser != null)
                IsAdmin = ((App)Application.Current).LoggedUser.IsAdmin;
            else
                IsAdmin = false;
            if (((App)Application.Current).LoggedUser != null)
                IsWriter = ((App)Application.Current).LoggedUser.UserTypeId == 1;
            else
                IsWriter = false;
        }
        public void Logout()
        {
            ((App)Application.Current).LoggedUser = null;
            ((App)Application.Current).MainPage = serviceProvider.GetService<SignPage>();
        }
    }
}
