using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class SeasonPastRacesView : ContentPage
{
	public SeasonPastRacesView(CurrSeasonRacesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}