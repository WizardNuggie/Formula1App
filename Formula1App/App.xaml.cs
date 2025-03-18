﻿using Formula1App.Models;
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
            MainPage = sp.GetService<SignPage>();
        }
    }
}
