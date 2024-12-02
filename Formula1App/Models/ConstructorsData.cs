using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.Models
{
    public class ConstructorsApi
    {
        public ConstructorsData ConstructorsData { get; set; }
    }

    public class ConstructorsData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public ConstructorTable ConstructorTable { get; set; }
    }

    public class ConstructorTable
    {
        public string season { get; set; }
        public Constructor[] Constructors { get; set; }
    }
}
