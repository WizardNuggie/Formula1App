using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class StandingsView : ContentPage
{
	public StandingsView(StandingsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}