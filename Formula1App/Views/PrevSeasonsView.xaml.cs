using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class PrevSeasonsView : ContentPage
{
	public PrevSeasonsView(PrevSeasonsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}