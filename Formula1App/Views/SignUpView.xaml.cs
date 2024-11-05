using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView(SignUpViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}