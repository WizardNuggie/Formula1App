using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.Models
{
    public class LoginUser
    {
        public string Username {  get; set; }
        public string Password { get; set; }

        public LoginUser(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
        public LoginUser() { }
    }
}
