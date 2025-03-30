using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class UpcomingRacesView : ContentPage
{
	public UpcomingRacesView(CurrSeasonRacesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}