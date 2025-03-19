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
        private Constructor constructor;
        public Constructor Constructor
        {
            get => constructor;
            set
            {
                constructor = value;
                OnPropertyChanged();
            }
        }
    }
}
