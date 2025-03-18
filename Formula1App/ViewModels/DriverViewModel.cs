using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ViewModels
{
    [QueryProperty(nameof(Driver), "Driver")]
    public class DriverViewModel : ViewModelsBase
    {
        private MyDriverStandings driver;
        public MyDriverStandings Driver
        {
            get => driver;
            set
            {
                driver = value;
                OnPropertyChanged();
            }
        }
        public DriverViewModel()
        {
        }
    }
}
