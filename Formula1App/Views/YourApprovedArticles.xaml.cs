using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class YourApprovedArticles : ContentPage
{
	public YourApprovedArticles(YourApprovedArticlesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}