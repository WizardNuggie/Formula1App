using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class AllNewsView : ContentPage
{
	public AllNewsView(AllNewsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}