using CommunityToolkit.Maui.Views;
using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class EditUserView : Popup
{
	public EditUserView(ManageUsersViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    private void ClosePopup(object sender, EventArgs e)
    {
        this.Close();
    }
}