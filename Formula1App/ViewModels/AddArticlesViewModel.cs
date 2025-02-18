using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Models;
using Formula1App.Services;
using Formula1App.ModelsExt;

namespace Formula1App.ViewModels
{
    public class AddArticlesViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
        private Article article;
        public Article Article
        {
            get => article;
            set
            {
                article = value;
                OnPropertyChanged();
            }
        }
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
        private List<Subject> selectedSubjects;
        public List<Subject> SelectedSubjects
        {
            get => selectedSubjects;
            set
            {
                selectedSubjects = value;
                OnPropertyChanged();
            }
        }
        public AddArticlesViewModel(IServiceProvider sp, F1IntService intService)
        {
            this.serviceProvider = sp;
            this.intService = intService;
            InitData();
        }
        private async void InitData()
        {
            await GetSubjects();
        }
        private async Task GetSubjects()
        {
            Subjects = await intService.GetSubjects();
        }
    }
}
