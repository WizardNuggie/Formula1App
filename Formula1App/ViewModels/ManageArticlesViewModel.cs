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
    public class ManageArticlesViewModel : ViewModelsBase
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
        public ICommand DeclineArticleCommand { get; set; }
        public ICommand ApproveArticleCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public ManageArticlesViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;
            Articles = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            ShowDetailsCommand = new Command(async () => ShowDetails());
            ApproveArticleCommand = new Command(async () => await ApproveArticle());
            DeclineArticleCommand = new Command(async () => await DeclineArticle());
            InitData();
        }
        private async void InitData()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetArticles();
            IsRefreshing = false;
        }
        private async Task GetArticles()
        {
            SelectedArticle = null;
            articles = await intService.GetAllNews();
            articles = articles.Where(a => a.StatusId == 2).ToList();
            Articles.Clear();
            foreach (Article a in articles)
            {
                a.FirstSubject = a.Subjects.FirstOrDefault();
                Articles.Add(a);
            }
        }
        private async Task ApproveArticle()
        {
            if (await AppShell.Current.DisplayAlert("Approve Article", "Are you sure you want to approve this article?", "OK", "Cancel"))
            {
                bool isApproved = await intService.ApproveArticle(SelectedArticle);
                if (isApproved)
                {
                    string success = $"The article \"{SelectedArticle.Title}\" was approved successfully";
                    AppShell.Current.DisplayAlert("Approval Of Article Succeeded", success, "OK");
                }
                else
                {
                    string err = "Something went wrong.\nPlease try again later";
                    AppShell.Current.DisplayAlert("Approval Of Article Failed", err, "OK");
                }
                await GetArticles();
            }
        }
        private async Task DeclineArticle()
        {
            if (await AppShell.Current.DisplayAlert("Decline Article", "Are you sure you want to decline this article?", "OK", "Cancel"))
            {
                bool isDeclined = await intService.DeclineArticle(SelectedArticle);
                if (isDeclined)
                {
                    string success = $"The article \"{SelectedArticle.Title}\" was declined successfully";
                    AppShell.Current.DisplayAlert("Declination Of Article Succeeded", success, "OK");
                }
                else
                {
                    string err = "Something went wrong.\nPlease try again later";
                    AppShell.Current.DisplayAlert("Declination Of Article Failed", err, "OK");
                }
                await GetArticles();
            }
        }
        private async void ShowDetails()
        {
            Dictionary<string, object> data = new();
            data.Add("Article", SelectedArticle);
            await AppShell.Current.GoToAsync("Article", data);
            SelectedArticle = null;
        }
    }
}
