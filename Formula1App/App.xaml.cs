using Formula1App.Models;
using Formula1App.Services;
using Formula1App.Styling;
using Formula1App.ViewModels;
using Formula1App.Views;
using Microsoft.Maui.Platform;

namespace Formula1App
{
    public partial class App : Application
    {
        public User LoggedUser { get; set; }
        public Dictionary<string, string> TeamColors;
        public Dictionary<string, string> CountryCodes;
        public Dictionary<string, string> CountryNames;
        public Dictionary<string, string> OffConstCodes;
        public Dictionary<string, string> OffConstNames;
        public string CurrYear;
        private F1IntService intService;
        private F1ExtService extService;
        public App(IServiceProvider sp, F1IntService f1IntService, F1ExtService f1ExtService)
        {
            this.intService = f1IntService;
            this.extService = f1ExtService;
            InitializeComponent();
            LoggedUser = null;
            TeamColors = new Dictionary<string, string>()
            {
                {"mclaren", "#FF8001"},
                {"red_bull", "#3671C6"},
                {"mercedes", "#28F4D2"},
                {"williams", "#1968DB"},
                {"aston_martin", "#229971"},
                {"sauber", "#52E253"},
                {"ferrari", "#E8252C"},
                {"alpine", "#00A1E8"},
                {"rb", "#6692FF"},
                {"haas", "#B6BABD"}
            };
            CountryCodes = new Dictionary<string, string>()
            {
                {"British", "GB"},
                {"Dutch", "NL"},
                {"Austrian", "AT"},
                {"German", "DE"},
                {"Italian", "IT"},
                {"Thai", "TH"},
                {"Canadian", "CA"},
                {"Swiss", "CH"},
                {"Monegasque", "MC"},
                {"Australian", "AU"},
                {"French", "FR"},
                {"Japanese", "JP"},
                {"American", "US"},
                {"New Zealander", "NZ"},
                {"Brazilian", "BR"},
                {"Spanish", "ES"},
                {"Argentine", "AR"},
                {"Chinese", "CN"},
                {"Finnish", "FI"}
            };
            CountryNames = new Dictionary<string, string>()
            {
                {"British", "GBR"},
                {"Dutch", "Netherlands"},
                {"Austrian", "Austria"},
                {"German", "Germany"},
                {"Italian", "Italy"},
                {"Thai", "Thailand"},
                {"Canadian", "Canada"},
                {"Swiss", "Switzerland"},
                {"Monegasque", "Monaco"},
                {"Australian", "Australia"},
                {"French", "France"},
                {"Japanese", "Japan"},
                {"American", "USA"},
                {"New Zealander", "New Zealand"},
                {"Brazilian", "Brazil"},
                {"Spanish", "Spain"},
                {"Argentine", "Argentina"},
                {"Chinese", "China"},
                {"Finnish", "Finland"}
            };
            OffConstCodes = new Dictionary<string, string>()
            {
                {"mclaren", "mclaren"},
                {"mercedes", "mercedes"},
                {"red_bull", "red-bull-racing"},
                {"williams", "williams"},
                {"aston_martin", "aston-martin"},
                {"sauber", "kick-sauber"},
                {"ferrari", "ferrari"},
                {"alpine", "alpine"},
                {"rb", "racing-bulls"},
                {"haas", "haas"}
            };
            OffConstNames = new Dictionary<string, string>()
            {
                {"mclaren", "McLaren"},
                {"mercedes", "Mercedes"},
                {"red_bull", "Red Bull Racing"},
                {"williams", "Williams"},
                {"aston_martin", "Aston Martin"},
                {"sauber", "Kick Sauber"},
                {"ferrari", "Ferrari"},
                {"alpine", "Alpine"},
                {"rb", "Racing Bulls"},
                {"haas", "Haas"}
            };
            CurrYear = DateTime.Now.Year.ToString();
            MainPage = sp.GetService<SignPage>();
        }
    }
}
