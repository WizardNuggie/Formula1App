using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1App.ModelsExt
{

    public class Driver
    {
        public string driverId { get; set; }
        public string permanentNumber { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string dateOfBirth { get; set; }
        public string nationality { get; set; }
        public string OffCode
        {
            get
            {
                if (string.IsNullOrEmpty(code))
                    return familyName.Substring(0, 3).ToUpper();
                else
                    return code;
            }
        }
    }
}
