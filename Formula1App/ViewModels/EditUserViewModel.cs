using Formula1App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Formula1App.ViewModels
{
    public partial class ManageUsersViewModel : ViewModelsBase
    {
        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                OnPropertyChanged();
                CheckUserDiff();
            }
        }
        private UserType selectedUTEdit;
        public UserType SelectedUTEdit
        {
            get => selectedUTEdit;
            set
            {
                selectedUTEdit = value;
                OnPropertyChanged();
                CheckUserDiff();
            }
        }
        private bool isUserDiff;
        public bool IsUserDiff
        {
            get => isUserDiff;
            set
            {
                isUserDiff = value;
                OnPropertyChanged();
                ((Command)SubmitChangesCommand).ChangeCanExecute();
            }
        }
        public ICommand SubmitChangesCommand { get; set; }
        private async Task SubmitChanges()
        {
            UserToEdit.IsAdmin = IsAdmin;
            UserToEdit.UserTypeId = SelectedUTEdit.Id;
            User u = await intService.EditUserDetails(UserToEdit);
            if (u != null)
            {
                AppShell.Current.DisplayAlert("Information Changed Successfully", "Your information were changed successfully.", "OK");
                await Refresh();
            }
            else
            {
                AppShell.Current.DisplayAlert("Something Went Wrong", "Something went wrong while updating the selected user's information.\nPlease try again later.", "OK");
            }
        }
        private async void CheckUserDiff()
        {
            if (UserToEdit.IsAdmin != IsAdmin ||
                UserToEdit.UserTypeId != selectedUTEdit.Id)
            {
                IsUserDiff = true;
            }
            else
                IsUserDiff = false;
        }
    }
}
