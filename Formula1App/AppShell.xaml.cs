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
            Routing.RegisterRoute("Register", typeof(SignUpView));
            Routing.RegisterRoute("AllNews", typeof(AllNewsView));
            Routing.RegisterRoute("Article", typeof(ArticleView));
            Routing.RegisterRoute("Manage", typeof(ManageUsersView));
            #endregion
        }
    }
}
