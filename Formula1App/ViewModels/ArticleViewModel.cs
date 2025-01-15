using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Services;
using Formula1App.Models;

namespace Formula1App.ViewModels
{
    [QueryProperty(nameof(Article), "Article")]
    public class ArticleViewModel:ViewModelsBase
    {
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
        public ArticleViewModel()
        {
        }
    }
}
