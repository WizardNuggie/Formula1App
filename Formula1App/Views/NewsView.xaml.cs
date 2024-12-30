using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class NewsView : ContentPage
{
	public NewsView(NewsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}