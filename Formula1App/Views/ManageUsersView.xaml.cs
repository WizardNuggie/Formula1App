using CommunityToolkit.Maui.Views;
using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class ManageUsersView : ContentPage
{
	public ManageUsersView(ManageUsersViewModel vm)
	{
		this.BindingContext = vm;
		vm.OpenPopUp += DisplayPopUp;
		InitializeComponent();
	}

	public void DisplayPopUp(List<string> l)
	{
		var popup = new EditUserView((ManageUsersViewModel)this.BindingContext);
		this.ShowPopup(popup);
	}
}