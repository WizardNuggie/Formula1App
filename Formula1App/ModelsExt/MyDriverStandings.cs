using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string TeamColor { get; set; }
        public string BackColor {  get; set; }
        public string TextColor { get; set; }
        public string ArrowColor { get; set; }
        public Constructor Constructor
        {
            get
            {
                return this.Constructors.Last();
            }
        }
    }
}
