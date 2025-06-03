using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.Models
{
    public class User
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
        public List<Article> Articles { get; set; }

        public Article Articles_
        {
            get => default;
            set
            {
            }
        }

        public User() { }
        public User(User u)
        {
            this.Id = u.Id;
            this.Email = u.Email;
            this.Username = u.Username;
            this.Name = u.Name;
            this.Password = u.Password;
            this.FavDriver = u.FavDriver;
            this.FavConstructor = u.FavConstructor;
            this.Birthday = u.Birthday;
            this.IsAdmin = u.IsAdmin;
            this.UserTypeId = u.UserTypeId;
            this.Articles = u.Articles != null ? new List<Article>(u.Articles) : new List<Article>();
        }
        public User(UserWType u)
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
            this.Articles = u.Articles;
        }
    }
}
