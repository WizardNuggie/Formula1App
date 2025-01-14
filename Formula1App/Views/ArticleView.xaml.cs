using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class ArticleView : ContentPage
{
	public ArticleView(ArticleViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}