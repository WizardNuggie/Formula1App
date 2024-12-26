using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class DriverStandingsView : ContentPage
{
	public DriverStandingsView(DriverStandingsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}