using Formula1App.ViewModels;
using Formula1App.Views;

namespace Formula1App
{
    public partial class App : Application
    {
        public App(LoginViewModel loginViewModel)
        {
            InitializeComponent();

            MainPage = new LoginView(loginViewModel);
        }
    }
}
