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
    public class AllNewsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;

        private List<Article> _articles;
        private ObservableCollection<Article> articles;
        public ObservableCollection<Article> Articles { get => articles; set { articles = value; OnPropertyChanged(); } }
        private List<Subject> subjects;
        public List<Subject> Subjects
        {
            get => subjects;
            set
            {
                subjects = value;
                OnPropertyChanged();
            }
        }
        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get => selectedSubject;
            set
            {
                selectedSubject = value;
                OnPropertyChanged();
                ((Command)ClearFilterCommand).ChangeCanExecute();
                Filter();
            }
        }
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
        public ICommand ClearFilterCommand { get; set; }

        public AllNewsViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;
            Articles = new();
            _articles = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            ClearFilterCommand = new Command(async () => await Refresh(), () => SelectedSubject != null);
            InitData();
        }

        private async void InitData()
        {
            await GetSubjects();
            await Refresh();
        }
        private async Task GetArticles()
        {
            List<Article> a = await intService.GetNews();
            _articles.Clear();
            _articles = new(a);
            Articles.Clear();
            foreach (Article ar in _articles)
            {
                Articles.Add(ar);
            }
        }
        private async Task GetSubjects()
        {
            Subjects = await intService.GetSubjects();
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            SelectedSubject = null;
            await GetArticles();
            IsRefreshing = false;
        }
        private async Task NavToArticle()
        {
            Dictionary<string, object> data = new();
            data.Add("Article", SelectedArticle);
            await AppShell.Current.GoToAsync("Article", data);
            SelectedArticle = null;
        }
        private async Task Filter()
        {
            Articles.Clear();
            SelectedArticle = null;
            if (SelectedSubject != null)
            {
                List<Article> a = await intService.GetNewsBySubject(SelectedSubject.Id);
                Articles = new(a);
            }
            else
            {
                await GetArticles();
            }
        }
    }
}
