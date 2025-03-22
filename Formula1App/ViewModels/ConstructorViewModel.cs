using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        private MyDriverStandings selectedDriver;
        public MyDriverStandings SelectedDriver
        {
            get => selectedDriver;
            set
            {
                selectedDriver = value;
                OnPropertyChanged();
                if (selectedDriver != null)
                    NavToDriver();
            }
        }
        //public ICommand NavToDriverCommand { get; set; }
        public ConstructorViewModel()
        {
            CurrYear = ((App)Application.Current).CurrYear;
            //NavToDriverCommand = new Command(async (Object obj) => await NavToDriver((MyDriverStandings)obj));
        }
        private async Task NavToDriver()
        {
            Dictionary<string, object> data = new();
            data.Add("Driver", SelectedDriver);
            await AppShell.Current.GoToAsync("Driver", data);
        }
    }
}
