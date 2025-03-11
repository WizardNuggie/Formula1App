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

        public User() { }
    }
}
