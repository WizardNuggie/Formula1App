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
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
                if (string.IsNullOrEmpty(searchText))
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
        public ICommand SearchCommand { get; set; }

        public ManageUsersViewModel(IServiceProvider serviceProvider, F1IntService intService)
        {
            this.serviceProvider = serviceProvider;
            this.intService = intService;
            users = new();
            Users = new();
            userTypes = new();
            UserTypes = new();
            IsRefreshing = false;
            RefreshCommand = new Command(async () => await Refresh());
            ClearFilterCommand = new Command(async () => await ClearFilter(), () => SelectedUt != null);
            SearchCommand = new Command(async () => await Search());
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
            SelectedUser = null;
            SelectedUt = null;
            SearchText = null;
            await GetUsers();
            IsRefreshing = false;
        }
        private async Task ClearFilter()
        {
            SelectedUser = null;
            SelectedUt = null;
        }
        private async Task GetUserTypes()
        {
            List<UserType> uts = await intService.GetUserTypes();
            uts.Add(new UserType()
            {
                Name = "Admin",
                Id = 100
            });
            UserTypes = new(uts);
        }
        private async Task FilterByUt()
        {
            Users.Clear();
            SelectedUser = null;
            if (SelectedUt != null)
            {
                if (SelectedUt.Id != 100)
                {
                    List<User> us = await intService.GetUsersByUT(SelectedUt.Id);
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        List<User> tempUs = new();
                        foreach (User u in us)
                        {
                            foreach (User user in users)
                            {
                                if (u.Id == user.Id)
                                    tempUs.Add(user);
                            }
                        }
                        users = new(tempUs);
                    }
                    else
                    {
                        users = new(us);
                    }
                    Users.Clear();
                    string ut = "";
                    foreach (User u in users)
                    {
                        ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                        Users.Add(new UserWType(u, ut));
                    }
                }
                else
                {
                    await GetUsers();
                    List<User> us = users.Where(x => x.IsAdmin == true).ToList();
                    Users.Clear();
                    users = new(us);
                    string ut = "";
                    foreach (User u in users)
                    {
                        ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                        Users.Add(new UserWType(u, ut));
                    }
                }
            }
            else if (string.IsNullOrEmpty(SearchText))
            {
                await GetUsers();
            }
            else
            {
                Search();
            }
        }
        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                if (SelectedUt == null)
                    await GetUsers();
                else
                    users = await intService.GetUsersByUT(SelectedUt.Id);
                List<User> us = users.Where(x => x.Username.Contains(SearchText)).ToList();
                users = new(us);
                Users.Clear();
                users = new(us);
                string ut = "";
                foreach (User u in users)
                {
                    ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                    Users.Add(new UserWType(u, ut));
                }
            }
            else
            {
                await FilterByUt();
            }
        }
    }
}
