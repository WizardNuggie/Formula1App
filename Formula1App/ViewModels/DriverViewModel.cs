using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public ICommand NavToTeamCommand { get; set; }
        public DriverViewModel()
        {
            CurrYear = ((App)Application.Current).CurrYear;
            NavToTeamCommand = new Command(await => NavToTeam());
        }
        private async Task NavToTeam()
        {
            Dictionary<string, object> data = new();
            data.Add("Constructor", Driver.Constructor);
            await AppShell.Current.GoToAsync("Constructor", data);
        }
    }
}
