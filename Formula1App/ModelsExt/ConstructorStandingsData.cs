using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class ConstructorStandingsApi
    {
        public ConstructorStandingsData ConstructorStandingsData { get; set; }
    }

    public class ConstructorStandingsData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Standingstable StandingsTable { get; set; }
    }

    public class Constructorstanding
    {
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public string wins { get; set; }
        public Constructor Constructor { get; set; }
    }

}
