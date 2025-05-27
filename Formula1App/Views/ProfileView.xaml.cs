using Formula1App.ViewModels;
using Formula1App.Services;

namespace Formula1App.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}