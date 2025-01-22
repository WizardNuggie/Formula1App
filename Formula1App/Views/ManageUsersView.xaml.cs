using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class ManageUsersView : ContentPage
{
	public ManageUsersView(ManageUsersViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}