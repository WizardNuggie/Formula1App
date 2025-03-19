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
                return ((App)Application.Current).OffConstCodes[constructorId];
            }
        }
        public string OfficialConstructorName
        {
            get
            {
                return ((App)Application.Current).OffConstNames[constructorId];
            }
        }
        public string PhotoUrl
        {
            get
            {
                return $"https://media.formula1.com/d_team_car_fallback_image.png/content/dam/fom-website/teams/{((App)Application.Current).CurrYear}/{OfficialConstructorId}";
            }
        }
    }

}
