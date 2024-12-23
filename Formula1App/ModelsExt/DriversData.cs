using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{
    public class DriverApi
    {
        public DriversData DriversData { get; set; }
    }

    public class DriversData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public DriverTable DriverTable { get; set; }
    }

    public class DriverTable
    {
        public string season { get; set; }
        public Driver[] Drivers { get; set; }
    }

}
