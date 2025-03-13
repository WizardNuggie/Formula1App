using Formula1App.Models;
using Formula1App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class YourDeclinedArticlesViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
        private Article selectedArticle;
        public Article SelectedArticle
        {
            get => selectedArticle;
            set
            {
                selectedArticle = value;
                OnPropertyChanged();
                if (selectedArticle != null)
                    NavToArticle();
            }
        }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        private List<Article> articles;
        public ObservableCollection<Article> Articles { get; private set; }
        public ICommand RefreshCommand { get; set; }

        public YourDeclinedArticlesViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;
            Articles = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            InitData();
        }
        private async void InitData()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            Articles.Clear();
            articles = ((App)Application.Current).LoggedUser.Articles.Where(a => a.StatusId == 3).ToList();//.Include(a => a.Subjects)
            foreach (Article a in articles)
            {
                a.FirstSubject = a.Subjects.FirstOrDefault();
                Articles.Add(a);
            }
            IsRefreshing = false;
        }
        private async Task NavToArticle()
        {
            Dictionary<string, object> data = new();
            data.Add("Article", SelectedArticle);
            await AppShell.Current.GoToAsync("Article", data);
            SelectedArticle = null;
        }
    }
}
