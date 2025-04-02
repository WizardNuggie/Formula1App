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
        public Dictionary<string, string> RacesCountryCodes;
        public Dictionary<string, string> CountryNames;
        public Dictionary<string, string> OffConstCodes;
        public Dictionary<string, string> OffConstNames;
        public Dictionary<string, string> MonthNames;
        public Dictionary<string, string> DaysInMonths;
        public Dictionary<string, string> DayNames;
        public Dictionary<string, string> RacesNames;
        public Dictionary<string, string> RacesGpName;
        public Dictionary<string, string> TrackPic;
        public Dictionary<string, string> TrackSponsors;
        public Dictionary<string, string> Statuses;
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
                {"Finnish", "FI"},
            };
            RacesCountryCodes = new Dictionary<string, string>()
            {
                {"Australia", "AU"},
                {"China", "CN"},
                {"Saudi Arabia", "SA"},
                {"Bahrain", "BH"},
                {"USA", "US"},
                {"Italy", "IT"},
                {"Monaco", "MC"},
                {"Spain", "ES"},
                {"Canada", "CA"},
                {"Austria", "AT"},
                {"UK", "GB"},
                {"Belgium", "BE"},
                {"Hungary", "HU"},
                {"Netherlands", "ND"},
                {"Azerbaijan", "AZ"},
                {"Singapore", "SG"},
                {"Mexico", "MX"},
                {"Brazil", "BR"},
                {"Qatar", "QA"},
                {"UAE", "AE"}
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
            MonthNames = new Dictionary<string, string>()
            {
                {"01", "Jan"},
                {"02", "Feb"},
                {"03", "Mar"},
                {"04", "Apr"},
                {"05", "May"},
                {"06", "Jun"},
                {"07", "Jul"},
                {"08", "Aug"},
                {"09", "Sep"},
                {"10", "Oct"},
                {"11", "Nov"},
                {"12", "Dec"}
            };
            DayNames = new Dictionary<string, string>()
            {
                {"Sunday", "Sun"},
                {"Monday", "Mon"},
                {"Tuesday", "Tue"},
                {"Wednesday", "Wed"},
                {"Thursday", "Thu"},
                {"Friday", "Fri"},
                {"Saturday", "Sat"},
            };
            RacesNames = new Dictionary<string, string>()
            {
                {"Melbourne", "Australia"},
                {"Shanghai", "China"},
                {"Suzuka", "Japan"},
                {"Sakhir", "Bahrain"},
                {"Jeddah", "Saudi Arabia"},
                {"Miami", "Miami"},
                {"Imola", "Emilia-Romagna"},
                {"Monte-Carlo", "Monaco"},
                {"Montmeló", "Spain"},
                {"Montreal", "Canada"},
                {"Spielberg", "Austria"},
                {"Silverstone", "Great Britain"},
                {"Spa", "Belgium"},
                {"Budapest", "Hungary"},
                {"Zandvoort", "Netherlands"},
                {"Monza", "Italy"},
                {"Baku", "Azerbaijan"},
                {"Marina Bay", "Singapore"},
                {"Austin", "United States"},
                {"Mexico City", "Mexico"},
                {"São Paulo", "Brazil"},
                {"Las Vegas", "Las Vegas"},
                {"Al Daayen", "Qatar"},
                {"Abu Dhabi", "Abu Dhabi"}
            };
            RacesGpName = new Dictionary<string, string>()
            {
                {"Melbourne", "Australian Grand Prix"},
                {"Shanghai", "Chinese Grand Prix"},
                {"Suzuka", "Japanese Grand Prix"},
                {"Sakhir", "Bahrain Grand Prix"},
                {"Jeddah", "Saudi Arabian Grand Prix"},
                {"Miami", "Miami Grand Prix"},
                {"Imola", "Gran Premio Del Made In Italy E Dell'Emilia-Romagna"},
                {"Monte-Carlo", "Grand Prix De Monaco"},
                {"Montmeló", "Gran Premio De España"},
                {"Montreal", "Grand Prix Du Canada"},
                {"Spielberg", "Austrian Grand Prix"},
                {"Silverstone", "British Grand Prix"},
                {"Spa", "Belgian Grand Prix"},
                {"Budapest", "Hungarian Grand Prix"},
                {"Zandvoort", "Dutch Grand Prix"},
                {"Monza", "Gran Premio D'Italia"},
                {"Baku", "Azerbaijan Grand Prix"},
                {"Marina Bay", "Singapore Grand Prix"},
                {"Austin", "United States Grand Prix"},
                {"Mexico City", "Gran Premio De La Ciudad De México"},
                {"São Paulo", "Grande Prémio De São Paulo"},
                {"Las Vegas", "Las Vegas Grand Prix"},
                {"Al Daayen", "Qatar Grand Prix"},
                {"Abu Dhabi", "Abu Dhabi Grand Prix"}
            };
            TrackPic = new Dictionary<string, string>()
            {
                {"Melbourne", "australia"},
                {"Shanghai", "china"},
                {"Suzuka", "japan"},
                {"Sakhir", "bahrain"},
                {"Jeddah", "saudi_arabia"},
                {"Miami", "miami"},
                {"Imola", "emilia%20romagna"},
                {"Monte-Carlo", "monaco"},
                {"Montmeló", "spain"},
                {"Montreal", "canada"},
                {"Spielberg", "austria"},
                {"Silverstone", "great%20britain"},
                {"Spa", "belgium"},
                {"Budapest", "hungary"},
                {"Zandvoort", "netherlands"},
                {"Monza", "italy"},
                {"Baku", "azerbaijan"},
                {"Marina Bay", "singapore"},
                {"Austin", "usa"},
                {"Mexico City", "mexico"},
                {"São Paulo", "brazil"},
                {"Las Vegas", "las%20vegas"},
                {"Al Daayen", "qatar"},
                {"Abu Dhabi", "abu%20dhabi"}
            };
            TrackSponsors = new Dictionary<string, string>()
            {
                {"Melbourne", "Louis Vuitton"},
                {"Shanghai", "Heineken"},
                {"Suzuka", "Lenovo"},
                {"Sakhir", "Gulf Air"},
                {"Jeddah", "STC"},
                {"Miami", "Crypto.com"},
                {"Imola", "AWS"},
                {"Monte-Carlo", "Tag Heuer"},
                {"Montmeló", "Aramco"},
                {"Montreal", "Pirelli"},
                {"Spielberg", "MSC Cruises"},
                {"Silverstone", "Qatar Airways"},
                {"Spa", "Moët & Chandon"},
                {"Budapest", "Lenovo"},
                {"Zandvoort", "Heineken"},
                {"Monza", "Pirelli"},
                {"Baku", "Qatar Airways"},
                {"Marina Bay", "Singapore Airlines"},
                {"Austin", "MSC Cruises"},
                {"Mexico City", ""},
                {"São Paulo", "MSC Cruises"},
                {"Las Vegas", "Heineken"},
                {"Al Daayen", "Qatar Airways"},
                {"Abu Dhabi", "Etihad Airways"}
            };
            Statuses = new Dictionary<string, string>()
            {
                {"Disqualified", "DSQ"},
                {"Did not start", "DNS"},
            };
            DaysInMonths = new Dictionary<string, string>()
            {
                {"01", "31"},
                {"02a", "28"},
                {"02b", "29"},
                {"03", "31"},
                {"04", "30"},
                {"05", "31"},
                {"06", "30"},
                {"07", "31"},
                {"08", "31"},
                {"09", "30"},
                {"10", "31"},
                {"11", "30"},
                {"12", "31"},
            };
            CurrYear = DateTime.Now.Year.ToString();
            MainPage = sp.GetService<SignPage>();
        }
    }
}
