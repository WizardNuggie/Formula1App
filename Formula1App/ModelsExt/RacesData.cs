using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class RacesApi
    {
        public RacesData RacesData { get; set; }
    }

    public class RacesData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public Racetable RaceTable { get; set; }
    }

    public class Racetable
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<Race> Races { get; set; }
    }

    public class Race
    {
        public string season { get; set; }
        public string round { get; set; }
        public string url { get; set; }
        public string raceName { get; set; }
        public Circuit Circuit { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string FirstDay
        {
            get
            {
                string day = DayFirst.Day.ToString();
                if (day.Length < 2)
                    return "0" + day;
                else
                    return day;
            }
        }
        public string LastDay
        {
            get
            {
                string day = DayLast.Day.ToString();
                if (day.Length < 2)
                    return "0" + day;
                else
                    return day;
            }
        }
        public List<Result> Results { get; set; }
        public List<Pitstop> PitStops { get; set; }
        public Result Winner
        {
            get
            {
                if (Results.FirstOrDefault() == null)
                    return new Result();
                else
                    return Results.FirstOrDefault();
            }
        }
        public DateOnly DayLast
        {
            get
            {
                return DateOnly.Parse(date);
            }
        }
        public DateOnly DayFirst
        {
            get
            {
                return DayLast.AddDays(-2);
            }
        }
        public Firstpractice FirstPractice { get; set; }
        public Secondpractice SecondPractice { get; set; }
        public Thirdpractice ThirdPractice { get; set; }
        public Qualifying Qualifying { get; set; }
        public Sprint Sprint { get; set; }
        public Sprintqualifying SprintQualifying { get; set; }
        public string Days
        {
            get
            {
                return $"{FirstDay}-{LastDay}";
            }
        }
        public string OffRaceName
        {
            get
            {
                if (Circuit.Location.locality == "All")
                    return Circuit.Location.locality;
                else if (((App)Application.Current).SpecialRacesNames.ContainsKey(raceName))
                    return ((App)Application.Current).SpecialRacesNames[raceName];
                else if (((App)Application.Current).RacesNames.ContainsKey(Circuit.Location.locality))
                    return ((App)Application.Current).RacesNames[Circuit.Location.locality];
                else
                    return Circuit.Location.locality;
            }
        }
        public string CircuitIcon
        {
            get
            {
                string name = "";
                if (this.OffRaceName.Contains(" "))
                {
                    name += OffRaceName.Substring(0, OffRaceName.IndexOf(" "));
                    name += "%20";
                    name += OffRaceName.Substring(OffRaceName.LastIndexOf(" ") + 1);
                }
                else
                {
                    name = OffRaceName;
                }
                return $"https://media.formula1.com/content/dam/fom-website/2018-redesign-assets/Track%20icons%204x3/{name}%20carbon.png";
            }
        }
        public string CircuitPic
        {
            get
            {
                if (((App)Application.Current).TrackPic.ContainsKey(Circuit.Location.locality))
                    return $"https://media.formula1.com/image/upload/f_auto,c_limit,w_1440,q_auto/f_auto/q_auto/content/dam/fom-website/2018-redesign-assets/Racehub%20header%20images%2016x9/{((App)Application.Current).TrackPic[Circuit.Location.locality]}";
                else
                    return "pic doesnt exist";
            }
        }
        public string FlagUrl
        {
            get
            {
                if (((App)Application.Current).RacesCountryCodes.ContainsKey(Circuit.Location.country))
                    return $"https://flagsapi.com/{((App)Application.Current).RacesCountryCodes[Circuit.Location.country]}/flat/64.png";
                else
                    return "error";
            }
        }
        public string OffGpName
        {
            get
            {
                if (((App)Application.Current).TrackSponsors.ContainsKey(Circuit.Location.locality))
                {
                    if (((App)Application.Current).SpecialRacesNames.ContainsKey(raceName))
                        return $"{((App)Application.Current).TrackSponsors[Circuit.Location.locality]} {raceName}";
                    else if (((App)Application.Current).RacesGpName.ContainsKey(Circuit.Location.locality))
                        return $"{((App)Application.Current).TrackSponsors[Circuit.Location.locality]} {((App)Application.Current).RacesGpName[Circuit.Location.locality]}";
                    else
                        return "error";
                }
                else
                    return "error";
            }
        }
        public string Month1
        {
            get
            {
                return ((App)Application.Current).MonthNames[DayFirst.Month.ToString()];
            }
        }
        public string Month2
        {
            get
            {
                return ((App)Application.Current).MonthNames[DayLast.Month.ToString()];
            }
        }
        public bool Has2Months
        {
            get
            {
                return Month1 != Month2;
            }
        }
        public bool Has1Month
        {
            get
            {
                return !Has2Months;
            }
        }
        public string MonthName
        {
            get
            {
                if (Has2Months)
                    return $"{Month1}-{Month2}";
                else
                    return Month1;
            }
        }
        public string MonthSize
        {
            get
            {
                if (MonthName.Contains('-'))
                    return "10";
                else
                    return "13";
            }
        }
        public bool HasSprint
        {
            get
            {
                return Sprint != null;
            }
        }
        public bool HasNoSprint
        {
            get
            {
                return !!HasSprint;
            }
        }
        public Race(Race r)
        {
            season = r.season;
            round = r.round;
            url = r.url;
            raceName = r.raceName;
            Circuit = new Circuit(r.Circuit);
            date = r.date;
            time = r.time;
            Results = r.Results;
            FirstPractice = r.FirstPractice;
            SecondPractice = r.SecondPractice;
            ThirdPractice = r.ThirdPractice;
            Qualifying = r.Qualifying;
            Sprint = r.Sprint;
            SprintQualifying = r.SprintQualifying;
        }
        public Race()
        {

        }
    }

    public class Circuit
    {
        public string circuitId { get; set; }
        public string url { get; set; }
        public string circuitName { get; set; }
        public Location Location { get; set; }
        public Circuit(Circuit c)
        {
            circuitId = c.circuitId;
            url = c.url;
            circuitName = c.circuitName;
            Location = new Location(c.Location);
        }
        public Circuit()
        {
            
        }
    }

    public class Location
    {
        public string lat { get; set; }
        public string _long { get; set; }
        public string locality { get; set; }
        public string country { get; set; }
        public Location(Location l)
        {
            lat = l.lat;
            _long = l._long;
            locality = l.locality;
            country = l.country;
        }
        public Location()
        {
            
        }
    }

    public class Firstpractice
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Secondpractice
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Thirdpractice
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Qualifying
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Sprint
    {
        public string date { get; set; }
        public string time { get; set; }
    }

    public class Sprintqualifying
    {
        public string date { get; set; }
        public string time { get; set; }
    }

}
