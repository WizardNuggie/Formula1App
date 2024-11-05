using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string FavDriver { get; set; }
        public string FavConstructor { get; set; }
        public DateOnly Birthday { get; set; }

        public User(string email, string username, string name, string password, string driver, string @const, DateOnly bd)
        {
            this.Email = email;
            this.Username = username;
            this.Name = name;
            this.Password = password;
            this.FavDriver = driver;
            this.FavConstructor = @const;
            this.Birthday = bd;
        }
        public User(Models.User u)
        {
            this.Email = u.Email;
            this.Username = u.Username;
            this.Name = u.Name;
            this.Password = u.Password;
            this.FavDriver = u.FavDriver;
            this.FavConstructor = u.FavConstructor;
            this.Birthday = u.Birthday;
        }
        public User() { }
    }
}
