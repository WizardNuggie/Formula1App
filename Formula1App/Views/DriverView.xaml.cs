using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class DriverView : ContentPage
{
	public DriverView(DriverViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}