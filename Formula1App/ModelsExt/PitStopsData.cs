using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class PitStopsApi
    {
        public PitStopsData PitStopsData { get; set; }
    }

    public class PitStopsData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Racetable RaceTable { get; set; }
    }
    public class Pitstop
    {
        public string driverId { get; set; }
        public string lap { get; set; }
        public string stop { get; set; }
        public string time { get; set; }
        public string duration { get; set; }
        public Driver Driver { get; set; }
    }

}
