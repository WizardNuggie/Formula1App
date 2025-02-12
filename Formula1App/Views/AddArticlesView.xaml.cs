using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class AddArticlesView : ContentPage
{
	public AddArticlesView(AddArticlesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}