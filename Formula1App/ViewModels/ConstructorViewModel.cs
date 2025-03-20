using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ViewModels
{
    [QueryProperty(nameof(Constructor), "Constructor")]
    public class ConstructorViewModel : ViewModelsBase
    {
        private Constructorstanding constructor;
        public Constructorstanding Constructor
        {
            get => constructor;
            set
            {
                constructor = value;
                OnPropertyChanged();
            }
        }
        private string currYear;
        public string CurrYear
        {
            get => currYear;
            set
            {
                currYear = value;
                OnPropertyChanged();
            }
        }
        public ConstructorViewModel()
        {
            CurrYear = ((App)Application.Current).CurrYear;
        }
    }
}
