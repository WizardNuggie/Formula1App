﻿using Formula1App.Services;
using Formula1App.Models;
using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Formula1App.ViewModels
{
    public class ConstStandingsViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;
        private readonly F1ExtService extService;

        private List<Constructorstanding> standings;
        public ObservableCollection<Constructorstanding> Standings { get; private set; }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        private Constructorstanding selectedConst;
        public Constructorstanding SelectedConst
        {
            get => selectedConst;
            set
            {
                selectedConst = value;
                OnPropertyChanged();
                if (selectedConst != null)
                    NavToConst(SelectedConst);
            }
        }
        private Constructorstanding firstPlace;
        public Constructorstanding FirstPlace
        {
            get => firstPlace;
            set
            {
                firstPlace = value;
                OnPropertyChanged();
            }
        }
        public ICommand RefreshCommand { get; set; }
        public ICommand GoToPrevStandings { get; set; }
        public ICommand GoToConstCommand { get; set; }

        public ConstStandingsViewModel(IServiceProvider sp, F1IntService intService, F1ExtService extService)
        {
            this.serviceProvider = sp;
            this.intService = intService;
            this.extService = extService;
            standings = new();
            Standings = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            GoToConstCommand = new Command(async (Object obj) => await NavToConst((Constructorstanding)obj));
            GoToPrevStandings = new Command(async () => await NavToPrevStands());
            InitData();
        }
        private async void InitData()
        {
            await Refresh();
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetStandings();
            IsRefreshing = false;
        }
        private async Task GetStandings()
        {
            standings = await extService.GetCurrConstructorsStandingsAsync();
            Standings.Clear();
            foreach (Constructorstanding cs in standings)
            {
                cs.Constructor.TeamColor = Color.FromArgb(((App)Application.Current).TeamColors[cs.Constructor.constructorId]);
                if (cs.positionText == "1")
                {
                    FirstPlace = cs;
                }
                else if (cs.positionText == "-")
                {
                    cs.positionText = "NC";
                    Standings.Add(cs);
                }
                else
                {
                    Standings.Add(cs);
                }
            }
        }
        private async Task NavToConst(Constructorstanding cs)
        {
            Dictionary<string, object> data = new();
            data.Add("Constructor", cs);
            await AppShell.Current.GoToAsync("Constructor", data);
            SelectedConst = null;
        }
        private async Task NavToPrevStands()
        {
            await AppShell.Current.GoToAsync("PrevSeasons");
        }
    }
}
