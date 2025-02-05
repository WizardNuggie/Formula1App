using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private List<UserType> userTypes;
        public List<UserType> UserTypes
        {
            get => userTypes;
            set
            {
                userTypes = value;
                OnPropertyChanged();
            }
        }
        private UserType selectedUt;
        public UserType SelectedUt
        {
            get => selectedUt;
            set
            {
                selectedUt = value;
                OnPropertyChanged();
                ((Command)ClearFilterCommand).ChangeCanExecute();
                FilterByUt();
            }
        }
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
        public ICommand ClearFilterCommand { get; set; }

        public ManageUsersViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;
            users = new();
            Users = new();
            Users.Clear();
            userTypes = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            ClearFilterCommand = new Command(async () => await Refresh(), () => SelectedUt != null);
            InitData();
        }
        private async void InitData()
        {
            await GetUserTypes();
            await Refresh();
        }
        private async Task GetUsers()
        {
            List<User> us = await intService.GetUsers();
            users.Clear();
            users = new(us);
            Users.Clear();
            string ut = "";
            foreach (User u in users)
            {
                ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                Users.Add(new UserWType(u, ut));
            }
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            await GetUsers();
            IsRefreshing = false;
        }
        private async Task GetUserTypes()
        {
            List<UserType> uts = await intService.GetUserTypes();
            userTypes = new(uts);
        }
        private async Task FilterByUt()
        {
            Users.Clear();
            SelectedUser = null;
            if (SelectedUt != null)
            {
                List<User> u = await intService.GetUsersByUt(SelectedUt.Id);//code this shit
                users = new(u);
            }
            else
            {
                await GetUsers();
            }
        }
    }
}
