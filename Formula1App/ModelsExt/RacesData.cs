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
        public List<Result> Results { get; set; }
        public Result Winner
        {
            get => Results.FirstOrDefault();
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
                string day1 = "";
                string day2 = "";
                day1 = date.Substring(date.Length - 2, 2);
                day2 = FirstPractice.date.Substring(date.Length - 2, 2);
                return $"{day1}-{day2}";
            }
        }
        public string OffRaceName
        {
            get
            {
                if (((App)Application.Current).RacesNames.ContainsKey(Circuit.Location.locality))
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
                return $"https://media.formula1.com/image/upload/f_auto,c_limit,w_1440,q_auto/f_auto/q_auto/content/dam/fom-website/2018-redesign-assets/Racehub%20header%20images%2016x9/{((App)Application.Current).TrackPic[Circuit.Location.locality]}";
            }
        }
        public string FlagUrl
        {
            get
            {
                return $"https://flagsapi.com/{((App)Application.Current).RacesCountryCodes[Circuit.Location.country]}/flat/64.png";
            }
        }
        public string OffGpName
        {
            get
            {
                return $"{((App)Application.Current).TrackSponsors[Circuit.Location.locality]} {((App)Application.Current).RacesGpName[Circuit.Location.locality]}".ToUpper();
            }
        }
        public string MonthName
        {
            get
            {
                int m1 = 0;
                int m2 = 0;
                int.TryParse(FirstPractice.date.Substring(5, 2), out m1);
                int.TryParse(date.Substring(5, 2), out m2);
                if (m1 < m2)
                    return $"{((App)Application.Current).MonthNames[m1.ToString()].ToUpper()}-{((App)Application.Current).MonthNames[m2.ToString()].ToUpper()}";
                else
                    return ((App)Application.Current).MonthNames[m1.ToString()].ToUpper();
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
    }

    public class Circuit
    {
        public string circuitId { get; set; }
        public string url { get; set; }
        public string circuitName { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public string lat { get; set; }
        public string _long { get; set; }
        public string locality { get; set; }
        public string country { get; set; }
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
