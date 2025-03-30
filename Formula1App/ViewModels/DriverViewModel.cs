using Formula1App.ModelsExt;
using Formula1App.Services;
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
        private readonly F1ExtService extService;
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
        public DriverViewModel(F1ExtService extService)
        {
            this.extService = extService;
            CurrYear = ((App)Application.Current).CurrYear;
            NavToTeamCommand = new Command(await => NavToTeam());
        }
        private async Task NavToTeam()
        {
            Dictionary<string, object> data = new();
            Constructorstanding cs = (await extService.GetCurrConstructorsStandingsAsync()).Where(x => x.Constructor.constructorId == Driver.Constructor.constructorId).FirstOrDefault();
            if (cs != null)
            {
                data.Add("Constructor", cs);
                await AppShell.Current.GoToAsync("Constructor", data);
            }
            else
            {
                await AppShell.Current.DisplayAlert("Something Went Wrong", "An unexpacted error occurred while trying to navigate to the constructor's page.\nPlease try again later", "OK");
            }
        }
    }
}
