using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Models;
using Formula1App.Services;
using Formula1App.ModelsExt;
using System.Windows.Input;

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
        private string photoPath;
        public string PhotoPath
        {
            get => photoPath;
            set
            {
                photoPath = value;
                OnPropertyChanged();
                if (string.IsNullOrEmpty(photoPath))
                {
                    BorderColor = Color.FromArgb("#C8C8C8");
                }
                else
                {
                    BorderColor = Color.FromArgb("#E11900");
                }
            }
        }
        private Color borderColor;
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
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
        public ICommand UploadPhotoCommand { get; set; }
        public ICommand SubmitArticleCommand { get; set; }
        public AddArticlesViewModel(IServiceProvider sp, F1IntService intService)
        {
            this.serviceProvider = sp;
            this.intService = intService;
            UploadPhotoCommand = new Command(UploadPhoto);
            SubmitArticleCommand = new Command(SubmitArticle);
            BorderColor = Color.FromArgb("#C8C8C8");
            Article = new();
            Subjects = new();
            SelectedSubjects = new();
            InitData();
        }
        private async void InitData()
        {
            await GetSubjects();
        }
        public async Task GetSubjects()
        {
            Subjects = await intService.GetSubjects();
        }
        private async void UploadPhoto()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo"
                });
                if (result != null)
                {
                    PhotoPath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private async void SubmitArticle()
        {
            if (string.IsNullOrEmpty(PhotoPath) || string.IsNullOrEmpty(Article.Title) || string.IsNullOrEmpty(Article.Text) || Subjects.Where(x => x.IsChecked).ToList().Count == 0)
            {
                string err = "You are missing one or more fields";
                AppShell.Current.DisplayAlert("Submission failed", err, "OK");
            }
            else
            {
                try
                {
                    SelectedSubjects.Clear();
                    InServerCall = true;
                    foreach (Subject s in Subjects)
                    {
                        if (s.IsChecked)
                        {
                            SelectedSubjects.Add(s);
                        }
                    }
                    Article.Subjects = SelectedSubjects;
                    Article result = await intService.UploadArticle(Article);
                    if (result == null)
                    {
                        string err = "The submission of the article failed.\nPlease try again later";
                        AppShell.Current.DisplayAlert("Submission failed", err, "OK");
                    }
                    else
                    {
                        bool isImageUploaded = await intService.UploadArticleImage(PhotoPath, result.Id);
                        ((App)Application.Current).LoggedUser.Articles.Add(result);
                        if (!isImageUploaded)
                        {
                            string err = "The article was submited but there was a problem submitting the image";
                            AppShell.Current.DisplayAlert("Image submission failed", err, "OK");
                            //build and redirect to image submission page
                        }
                        else
                        {
                            string succ = "Submission completed successfully.\nYou will now be redirected to your pending articles page";
                            AppShell.Current.DisplayAlert("Submission succeeded", succ, "OK");
                            await AppShell.Current.GoToAsync("///Pending");
                        }
                    }
                    InServerCall = false;
                }
                catch (Exception ex)
                {
                    string err = "The submission of the article failed.\nPlease try again later";
                    AppShell.Current.DisplayAlert("Submission failed", err, "OK");
                }
            }
        }
    }
}
