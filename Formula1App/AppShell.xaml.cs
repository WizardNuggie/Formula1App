using Formula1App.ViewModels;
using Formula1App.Views;

namespace Formula1App
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
            #region Routing
            Routing.RegisterRoute("Login", typeof(LoginView));
            Routing.RegisterRoute("Register", typeof(SignUpView));
            Routing.RegisterRoute("AllNews", typeof(AllNewsView));
            Routing.RegisterRoute("Article", typeof(ArticleView));
            Routing.RegisterRoute("Driver", typeof(DriverView));
            #endregion
        }
    }
}
