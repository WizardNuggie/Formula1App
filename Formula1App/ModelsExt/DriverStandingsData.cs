using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class DriverStandingsApi
    {
        public DriverStandingsData DriverStandingsData { get; set; }
    }

    public class DriverStandingsData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Standingstable StandingsTable { get; set; }
    }

    public class Standingstable
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<Standingslist> StandingsLists { get; set; }

        public Standingslist Standingslist
        {
            get => default;
            set
            {
            }
        }
    }

    public class Standingslist
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<Driverstanding> DriverStandings { get; set; }
        public List<Constructorstanding> ConstructorStandings { get; set; }

        public Constructorstanding Constructorstanding
        {
            get => default;
            set
            {
            }
        }

        public Driverstanding Driverstanding
        {
            get => default;
            set
            {
            }
        }
    }

    public class Driverstanding
    {
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public string wins { get; set; }
        public Driver Driver { get; set; }
        public List<Constructor> Constructors { get; set; }

        public Constructor Constructor
        {
            get => default;
            set
            {
            }
        }
    }
}
