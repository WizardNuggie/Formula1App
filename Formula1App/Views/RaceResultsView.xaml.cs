using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class RaceResultsView : ContentPage
{
	public RaceResultsView(CurrSeasonRacesViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}