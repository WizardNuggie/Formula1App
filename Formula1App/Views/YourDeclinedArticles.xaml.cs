using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class YourDeclinedArticles : ContentPage
{
	public YourDeclinedArticles(YourDeclinedArticlesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}