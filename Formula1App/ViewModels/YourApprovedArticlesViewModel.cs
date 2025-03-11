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
    public class YourApprovedArticlesViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
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

        public YourApprovedArticlesViewModel(IServiceProvider serviceProvider, F1IntService intService)
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
            articles = ((App)Application.Current).LoggedUser.Articles.Where(a => a.StatusId == 1).ToList();//.Include(a => a.Subjects)
            foreach (Article a in articles)
            {
                a.FirstSubject = a.Subjects.FirstOrDefault();
                Articles.Add(a);
            }
            IsRefreshing = false;
        }
    }
}
