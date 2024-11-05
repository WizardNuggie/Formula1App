using Formula1App.Views;

namespace Formula1App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            #region Routing
            Routing.RegisterRoute("Login", typeof(LoginView));
            #endregion
        }
    }
}
