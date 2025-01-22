using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Models;
using Formula1App.Services;

namespace Formula1App.ModelsExt
{
    public class UserWType
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string FavDriver { get; set; }
        public string FavConstructor { get; set; }
        public DateOnly Birthday { get; set; }
        public bool IsAdmin { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }

        public UserWType() { }
        public UserWType(User u, F1IntService service)
        {
            this.Id = u.Id;
            this.Email = u.Email;
            this.Username = u.Username;
            this.Password = u.Password;
            this.Name = u.Name;
            this.Password = u.Password;
            this.FavDriver = u.FavDriver;
            this.FavConstructor = u.FavConstructor;
            this.Birthday = u.Birthday;
            this.IsAdmin = u.IsAdmin;
            this.UserTypeId = u.UserTypeId;
            List<UserType> l = new();
            GetTypeData(l, service);
        }
        private async void GetTypeData(List<UserType> l, F1IntService service)
        {
            await GetTypeName(l, service);
        }
        private async Task GetTypeName(List<UserType> l, F1IntService service)
        {
            l = await service.GetUsertypes();
            this.UserTypeName = l.Where(u => u.Id == this.UserTypeId).FirstOrDefault().Name;
        }
    }
}
