using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class ManageArticlesView : ContentPage
{
	public ManageArticlesView(ManageArticlesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}