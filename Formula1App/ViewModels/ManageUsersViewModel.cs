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
        private string searchId;
        public string SearchId
        {
            get => searchId;
            set
            {
                searchId = value;
                OnPropertyChanged();
                if (string.IsNullOrEmpty(searchId) && inIdSearch)
                    IdSearch();
            }
        }
        private bool inIdSearch;
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
        public ICommand IdSearchCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ShowDetailsCommand { get; set; }
        public ICommand EditUserCommand { get; set; }

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
            IdSearchCommand = new Command(async () => await IdSearch());
            RemoveCommand = new Command((Object obj) => RemoveUser((UserWType)obj));
            ShowDetailsCommand = new Command((Object obj) => ShowDetails((UserWType)obj));
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
            if (!inIdSearch)
            {
                Users.Clear();
                string ut = "";
                foreach (User u in users)
                {
                    ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                    Users.Add(new UserWType(u, ut));
                }
            }
        }
        private async Task Refresh()
        {
            IsRefreshing = true;
            SelectedUt = null;
            SearchText = null;
            SearchId = null;
            await GetUsers();
            IsRefreshing = false;
        }
        private async Task ClearFilter()
        {
            SelectedUt = null;
            Search();
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
            if (SelectedUt != null)
            {
                if (inIdSearch)
                    SearchId = "";
                if (!string.IsNullOrEmpty(SearchText))
                    await Search();
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
                    if (string.IsNullOrEmpty(SearchText))
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
                if (inIdSearch)
                    SearchId = "";
                else if (SelectedUt == null)
                    await GetUsers();
                if (SelectedUt != null)
                {
                    if (SelectedUt.Id != 100)
                        users = await intService.GetUsersByUT(SelectedUt.Id);
                    else
                    {
                        await GetUsers();
                        users = users.Where(x => x.IsAdmin == true).ToList();
                    }
                }
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
        private async Task IdSearch()
        {
            if (!string.IsNullOrEmpty(SearchId))
            {
                inIdSearch = true;
                SearchText = "";
                SelectedUt = null;
                await GetUsers();
                Users.Clear();
                List<User> us = users.Where(x => x.Id.ToString() == SearchId).ToList();
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
                if (inIdSearch)
                {
                    inIdSearch = false;
                    await GetUsers();
                }
                else
                    await Search();
            }
        }
        private async Task RemoveUser(UserWType user)
        {
            bool willDelete = await AppShell.Current.DisplayAlert("Delete user", $"Are you sure you want to delete the user \"{user.Username}\" ?", "OK", "Cancel");
            if (willDelete)
            {
                User u = users.Where(x => x.Id == user.Id).FirstOrDefault();
                if (u != null)
                {
                    bool isRemoved = await intService.RemoveUser(u);
                    if (isRemoved)
                    {
                        users.Remove(u);
                        Users.Remove(user);
                        string success = $"the user \"{user.Username}\" was deleted successfully";
                        AppShell.Current.DisplayAlert("Deletion of user succeeded", success, "OK");
                    }
                    else
                    {
                        string err = "Something went wrong.\nPlease try again later";
                        AppShell.Current.DisplayAlert("Deletion of user failed", err, "OK");
                    }
                }
                else
                {
                    string err = "User was not found in database.\nPlease try again later";
                    AppShell.Current.DisplayAlert("Deletion of user failed", err, "OK");
                }
            }
            else
            {
                return;
            }
        }
        private void ShowDetails(UserWType u)
        {
            string details = $"ID: \"{u.Id}\"\nUsername: \"{u.Username}\"\nPassword: \"{u.Password}\"\nEmail: \"{u.Email}\"\nName: \"{u.Name}\"\nFavourite Driver: \"{u.FavDriver}\"\nFavourite Constructor: \"{u.FavConstructor}\"\nDate of Birth: \"{u.Birthday.ToString()}\"\nIs Admin: \"{u.IsAdmin}\"\nUser Type: \"{u.UserTypeName}\"";
            AppShell.Current.DisplayAlert($"{u.Username}'s Details", details, "OK");
        }
    }
}
