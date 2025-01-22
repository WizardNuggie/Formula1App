using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1App.Models;

namespace Formula1App.ModelsExt
{
    public class ResponseUser
    {
        public User User { get; set; }
        public bool IsExist { get; set; }
        public ResponseUser() { }
    }
}
