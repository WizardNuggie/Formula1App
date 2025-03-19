using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Formula1App.ModelsExt
{
    public class MyDriverStandings
    {
        public string DriverId { get; set; }
        public string PermanentNumber { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public string PositionText { get; set; }
        public string Points { get; set; }
        public string Wins { get; set; }
        public Constructor[] Constructors { get; set; }
        public Color TeamColor { get; set; }
        public Constructor Constructor
        {
            get
            {
                return this.Constructors.Last();
            }
        }
        public string CountryName
        {
            get
            {
                return ((App)Application.Current).CountryNames[Nationality];
            }
        }
        public string NationalityFlag
        {
            get
            {
                return $"https://flagsapi.com/{((App)Application.Current).CountryCodes[Nationality]}/flat/64.png";
            }
        }
        public string PhotoUrl
        {
            get
            {
                string fName = "";
                if (this.FirstName.Contains(" "))
                {
                    fName += FirstName.Substring(0, FirstName.IndexOf(" "));
                    fName += "%20";
                    fName += FirstName.Substring(FirstName.LastIndexOf(" ")+1);
                }
                else
                {
                    fName = FirstName;
                }
                string combined = (FirstName.Substring(0, 3) + LastName.Substring(0, 3)).ToLower();
                return $"https://media.formula1.com/d_driver_fallback_image.png/content/dam/fom-website/drivers/{FirstName[0].ToString().ToUpper()}/{combined.ToUpper()}01_{fName}_{LastName}/{combined}01";
            }
        }
        public string NumberLogoUrl
        {
            get
            {
                string combined = (FirstName.Substring(0, 3) + LastName.Substring(0, 3)).ToUpper();
                return $"https://media.formula1.com/d_default_fallback_image.png/content/dam/fom-website/2018-redesign-assets/drivers/number-logos/{combined}01.png";
            }
        }
        public string UpperLastName
        {
            get
            {
                return LastName.ToUpper();
            }
        }
        public string Dob
        {
            get
            {
                return $"{DateOfBirth.Substring(8, 2)}/{DateOfBirth.Substring(5, 2)}/{DateOfBirth.Substring(0, 4)}";
            }
        }
    }
}
