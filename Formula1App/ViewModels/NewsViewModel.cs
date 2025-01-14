using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Services;
using Formula1App.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class NewsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1ExtService extService;
        private readonly F1IntService intService;

        private List<Article> articles;
        public ObservableCollection<Article> Articles { get; private set; }
        //private List<Subject> subjects;
        //public List<Subject> Subjects
        //{
        //    get => subjects;
        //    set
        //    {
        //        subjects = value;
        //        OnPropertyChanged();
        //    }
        //}
        //private Subject selectedSubject;
        //public Subject SelectedSubject
        //{
        //    get => selectedSubject;
        //    set
        //    {
        //        selectedSubject = value;
        //        OnPropertyChanged();
        //        Filter();
        //    }
        //}
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

        public ICommand RefreshCommand { get; set; }
        public ICommand NavToArticleCommand { get; set; }

        public NewsViewModel(IServiceProvider serviceProvider, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.extService = extService;
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
        private async Task GetArticles()
        {
            List<Article> a = await intService.GetNews();
            articles = new(a);
        }
        //private async Task GetSubjects()
        //{
        //    Subjects = await intService.GetSubjects();
        //}
        private async Task Refresh()
        {
            IsRefreshing = true;
            Articles.Clear();
            await GetArticles();
            foreach (Article a in articles)
            {
                Articles.Add(a);
            }
            IsRefreshing = false;
        }
        private async Task NavToArticle()
        {
            Dictionary<string, object> data = new();
            data.Add("Article", SelectedArticle);
            await AppShell.Current.GoToAsync("DriverStandings", data);
            SelectedArticle = null;
        }
        //private async Task Filter()
        //{
        //    Articles.Clear();
        //    SelectedArticle = null;
        //    if (SelectedSubject != null)
        //    {
        //        List<Article> a = await intService.GetNewsBySubject(SelectedSubject.Id);
        //        articles = new(a);
        //    }
        //    else
        //    {
        //        GetArticles();
        //    }
        //    foreach (Article a in articles)
        //    {
        //        Articles.Add(a);
        //    }
        //}
    }
}
