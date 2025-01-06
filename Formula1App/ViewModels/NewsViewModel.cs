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
        public List<Subject> Subjects { get; private set; }
        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get => selectedSubject;
            set
            {
                selectedSubject = value;
                OnPropertyChanged();
                Filter();
                ((Command)ClearFilterCommand).ChangeCanExecute();
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

        public NewsViewModel(IServiceProvider serviceProvider, F1ExtService extService, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.extService = extService;
            this.intService = intService;

            Subjects = new();
            GetSubjects();
            GetArticles();
            Articles = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            ClearFilterCommand = new Command(async () => await Refresh(), () => SelectedSubject != null);
        }
        private async void GetArticles()
        {
            articles = await intService.GetNews();
        }
        private async void GetSubjects()
        {
            Subjects = await intService.GetSubjects();
        }
        private async Task Filter()
        {
            Articles.Clear();
            SelectedArticle = null;
            GetArticles();
            foreach (Article a in articles)
            {
                if (a.SubjectId == SelectedStatus.Id)
                    Questions.Add(question);
            }
        }
    }
}
