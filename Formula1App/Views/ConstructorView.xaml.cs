using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class ConstructorView : ContentPage
{
	public ConstructorView(ConstructorViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}