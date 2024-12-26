using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class ConstStandingsView : ContentPage
{
	public ConstStandingsView(ConstStandingsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}