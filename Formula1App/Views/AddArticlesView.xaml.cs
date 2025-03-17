using Formula1App.Models;
using Formula1App.ViewModels;

namespace Formula1App.Views;

public partial class AddArticlesView : ContentPage
{
    public AddArticlesView(AddArticlesViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AddArticlesViewModel context = (AddArticlesViewModel)this.BindingContext;
        context.Article = new();
        foreach (Subject s in context.Subjects)
        {
            s.IsChecked = false;
        }
        context.SelectedSubjects = new();
        context.BorderColor = Color.FromArgb("#C8C8C8");
    }
}