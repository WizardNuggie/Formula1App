namespace Formula1App.Views;

public partial class SignPage : TabbedPage
{
	public SignPage(LoginView login, SignUpView signup)
	{
		InitializeComponent();
		this.Children.Add(login);
        this.Children.Add(signup);
    }
}