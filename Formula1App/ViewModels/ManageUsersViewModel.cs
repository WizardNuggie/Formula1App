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
    public partial class ManageUsersViewModel : ViewModelsBase
    {
        public event Action<List<string>> OpenPopUp;
        private readonly IServiceProvider serviceProvider;
        private readonly F1IntService intService;

        private List<User> UsersHolder;
        private List<User> usersLst;
        private ObservableCollection<UserWType> users;
        public ObservableCollection<UserWType> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged();
            }
        }
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
                if (string.IsNullOrEmpty(searchText) && !inIdSearch)
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
        private User userToEdit;
        public User UserToEdit
        {
            get => userToEdit;
            set
            {
                userToEdit = value;
                OnPropertyChanged();
            }
        }
        private List<UserType> uts;
        public List<UserType> Uts
        {
            get => uts;
            set
            {
                uts = value;
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
            usersLst = new();
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
            EditUserCommand = new Command((Object obj) => EditUser((UserWType)obj));
            SubmitChangesCommand = new Command(async () => await SubmitChanges(), () => IsUserDiff);
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
            UsersHolder = new(us);
            usersLst.Clear();
            usersLst = new(us);
            if (!inIdSearch)
            {
                Users.Clear();
                string ut = "";
                foreach (User u in usersLst)
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
            List<UserType> utsTemp = await intService.GetUserTypes();
            Uts = new(utsTemp);
            utsTemp.Add(new UserType()
            {
                Name = "Admin",
                Id = 100
            });
            UserTypes = new(utsTemp);
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
                if (SelectedUt.Name.ToLower() != "admin")
                {
                    List<User> us = await intService.GetUsersByUT(SelectedUt.Id);
                    if (!string.IsNullOrEmpty(SearchText))
                    {
                        List<User> tempUs = new();
                        foreach (User u in us)
                        {
                            foreach (User user in usersLst)
                            {
                                if (u.Id == user.Id)
                                    tempUs.Add(user);
                            }
                        }
                        usersLst = new(tempUs);
                    }
                    else
                    {
                        usersLst = new(us);
                    }
                    Users.Clear();
                    string ut = "";
                    foreach (User u in usersLst)
                    {
                        ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                        Users.Add(new UserWType(u, ut));
                    }
                }
                else
                {
                    List<User> us = new();
                    if (string.IsNullOrEmpty(SearchText))
                    {
                        us = UsersHolder.Where(x => x.IsAdmin == true).ToList();
                    }
                    else
                    {
                        us = usersLst.Where(x => x.IsAdmin == true).ToList();
                    }
                    Users.Clear();
                    usersLst = new(us);
                    string ut = "";
                    foreach (User u in usersLst)
                    {
                        ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                        Users.Add(new UserWType(u, ut));
                    }
                }
            }
            else if (string.IsNullOrEmpty(SearchText))
            {
                usersLst = new(UsersHolder);
                Users.Clear();
                string ut = "";
                foreach (User u in usersLst)
                {
                    ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                    Users.Add(new UserWType(u, ut));
                }
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
                    usersLst = new(UsersHolder);
                if (SelectedUt != null)
                {
                    if (SelectedUt.Id != 100)
                        usersLst = await intService.GetUsersByUT(SelectedUt.Id);
                    else
                    {
                        usersLst = new(UsersHolder);
                        usersLst = usersLst.Where(x => x.IsAdmin == true).ToList();
                    }
                }
                List<User> us = usersLst.Where(x => x.Username.Contains(SearchText)).ToList();
                usersLst = new(us);
                Users.Clear();
                string ut = "";
                foreach (User u in usersLst)
                {
                    ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                    Users.Add(new UserWType(u, ut));
                }
            }
            else
            {
                if (!inIdSearch)
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
                Users.Clear();
                List<User> us = UsersHolder.Where(x => x.Id.ToString() == SearchId).ToList();
                usersLst = new(us);
                string ut = "";
                foreach (User u in usersLst)
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
                    usersLst = new(UsersHolder);
                    Users.Clear();
                    string ut = "";
                    foreach (User u in usersLst)
                    {
                        ut = userTypes.Where(x => x.Id == u.UserTypeId).FirstOrDefault().Name;
                        Users.Add(new UserWType(u, ut));
                    }
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
                User u = usersLst.Where(x => x.Id == user.Id).FirstOrDefault();
                if (u != null)
                {
                    bool isRemoved = await intService.RemoveUser(u);
                    if (isRemoved)
                    {
                        usersLst.Remove(u);
                        Users.Remove(user);
                        string success = $"the user \"{user.Username}\" was deleted successfully";
                        AppShell.Current.DisplayAlert("Deletion Of User Succeeded", success, "OK");
                    }
                    else
                    {
                        string err = "Something went wrong.\nPlease try again later";
                        AppShell.Current.DisplayAlert("Deletion Of User Failed", err, "OK");
                    }
                }
                else
                {
                    string err = "User was not found in database.\nPlease try again later";
                    AppShell.Current.DisplayAlert("Deletion Of User Failed", err, "OK");
                }
            }
            else
            {
                return;
            }
        }
        private async void EditUser(UserWType user)
        {
            UserToEdit = new(user);
            SelectedUTEdit = UserTypes.Where(x => x.Id == UserToEdit.UserTypeId).FirstOrDefault();
            IsAdmin = UserToEdit.IsAdmin;
            if (OpenPopUp != null)
            {
                List<string> l = new();
                OpenPopUp(l);
            }
        }
        private void ShowDetails(UserWType u)
        {
            string details = $"ID: \"{u.Id}\"\nUsername: \"{u.Username}\"\nPassword: \"{u.Password}\"\nEmail: \"{u.Email}\"\nName: \"{u.Name}\"\nFavourite Driver: \"{u.FavDriver}\"\nFavourite Constructor: \"{u.FavConstructor}\"\nDate of Birth: \"{u.Birthday.ToString()}\"\nIs Admin: \"{u.IsAdmin}\"\nUser Type: \"{u.UserTypeName}\"";
            AppShell.Current.DisplayAlert($"{u.Username}'s Details", details, "OK");
        }
    }
}
