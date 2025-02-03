using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            if (((App)Application.Current).LoggedUser != null)
                IsAdmin = ((App)Application.Current).LoggedUser.IsAdmin;
            else
                IsAdmin = false;
        }
    }
}
