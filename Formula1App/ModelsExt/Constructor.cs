using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class Constructor
    {
        public string constructorId { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string nationality { get; set; }
        public string OfficialConstructorId
        {
            get
            {
                if (((App)Application.Current).OffConstCodes.ContainsKey(constructorId))
                    return ((App)Application.Current).OffConstCodes[constructorId];
                else
                    return constructorId;
            }
        }
        public string OfficialConstructorName
        {
            get
            {
                if (((App)Application.Current).OffConstNames.ContainsKey(constructorId))
                    return ((App)Application.Current).OffConstNames[constructorId];
                else
                    return name;
            }
        }
        public string PhotoUrl
        {
            get
            {
                return $"https://media.formula1.com/d_team_car_fallback_image.png/content/dam/fom-website/teams/{((App)Application.Current).CurrYear}/{OfficialConstructorId}";
            }
        }
        public string LogoPhotoUrl
        {
            get
            {
                return $"https://media.formula1.com/content/dam/fom-website/teams/2025/{OfficialConstructorId}-logo.png";
            }
        }
        public string FullLogoPhotoUrl
        {
            get
            {
                string start = "https://media.formula1.com/image/upload/f_auto,c_limit,q_75,w_1320/";
                string cont = "content/dam/";
                string end = "fom-website/2018-redesign-assets/team%20logos/";
                string idUrl = OfficialConstructorId;
                if (idUrl.Contains("-"))
                {
                    idUrl = idUrl.Replace("-", "%20");
                }
                string str = start;
                if (constructorId == "rb")
                {
                    str += $"{end}{idUrl}";
                }
                else
                {
                    str += $"{cont}{end}{idUrl}";
                }
                return str;
            }
        }
        public string CountryName
        {
            get
            {
                return ((App)Application.Current).CountryNames[nationality];
            }
        }
        public string NationalityFlag
        {
            get
            {
                return $"https://flagsapi.com/{((App)Application.Current).CountryCodes[nationality]}/flat/64.png";
            }
        }
        public Color TeamColor { get; set; }
        public List<MyDriverStandings> Drivers { get; set; }
    }

}
