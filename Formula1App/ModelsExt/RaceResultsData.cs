using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class RaceResultsApi
    {
        public RaceResultsData RaceResultsData { get; set; }
    }

    public class RaceResultsData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Racetable RaceTable { get; set; }
    }


    public class Result
    {
        public string number { get; set; }
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
        public string grid { get; set; }
        public string laps { get; set; }
        public string status { get; set; }
        public Time Time { get; set; }
        public Fastestlap FastestLap { get; set; }
    }

    public class Time
    {
        public string millis { get; set; }
        public string time { get; set; }
    }

    public class Fastestlap
    {
        public string rank { get; set; }
        public string lap { get; set; }
        public Time1 Time { get; set; }
    }

    public class Time1
    {
        public string time { get; set; }
    }

}
