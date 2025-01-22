using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Media;
using Formula1App.Models;
using Formula1App.ModelsExt;
using Formula1App.Services;

namespace Formula1App.ViewModels
{
    public class ManageUsersViewModel : ViewModelsBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;

        private List<User> users;
        public ObservableCollection<UserWType> Users { get; private set; }
        private UserWType selectedUser;
        public UserWType SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }
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

        public ICommand RefreshCommand { get; set; }

        public ManageUsersViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;
            users = new();
            Users = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            InitData();
        }
        private async void InitData()
        {
            await Refresh();
        }
        private async Task GetUsers()
        {
            users = await intService.GetUsers();
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetUsers();
            foreach (User u in users)
            {
                Users.Add(new UserWType(u, intService));
            }
            IsRefreshing = false;
        }
    }
}
