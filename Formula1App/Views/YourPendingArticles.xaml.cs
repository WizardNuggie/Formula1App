using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class YourPendingArticles : ContentPage
{
	public YourPendingArticles(YourPendingArticlesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}