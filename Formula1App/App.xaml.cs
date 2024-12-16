using Formula1App.Models;
using Formula1App.Services;
using Formula1App.ViewModels;
using Formula1App.Views;

namespace Formula1App
{
    public partial class App : Application
    {
        public User LoggedUser { get; set; }
        private F1IntService intService;
        private F1ExtService extService;
        public App(IServiceProvider sp, F1IntService f1IntService, F1ExtService f1ExtService)
        {
            this.intService = f1IntService;
            this.extService = f1ExtService;
            InitializeComponent();
            LoggedUser = null;

            MainPage = sp.GetService<SignPage>();
        }
    }
}
