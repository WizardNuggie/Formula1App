﻿using System;
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
        private readonly F1IntService intService;

        private List<Article> articles;
        public ObservableCollection<Article> Articles { get; private set; }
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
        public ICommand NavToAllNewsCommand { get; set; }

        public NewsViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;            
            Articles = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            NavToAllNewsCommand = new Command(async () => await NavToAllNews());
            InitData();
        }

        private async void InitData()
        {
            await Refresh();
        }
        private async Task GetArticles()
        {
            List<Article> a = await intService.GetNews();
            a = a.Take(5).ToList();
            articles = new(a);
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetArticles();
            Articles.Clear();
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
            await AppShell.Current.GoToAsync("Article", data);
            SelectedArticle = null;
        }
        private async Task NavToAllNews()
        {
            await AppShell.Current.GoToAsync("AllNews");
            SelectedArticle = null;
        }
    }
}
