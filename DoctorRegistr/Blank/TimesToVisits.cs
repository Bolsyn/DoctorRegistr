using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DoctorRegistr.Blank
{
    public class TimesToVisits
    {
        CultureInfo enUS = new CultureInfo("en-US");
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime TimeOfVisit { 
            get 
            {
                return TimeOfVisit;
            }
            set 
            {
                if (value < DateTime.ParseExact("9:00", "HH:mm", enUS ) || value > DateTime.ParseExact("14:00", "HH:mm", enUS))
                {
                    this.TimeOfVisit = value;
                }
                else if (value < DateTime.ParseExact("12:00", "HH:mm", enUS) || value > DateTime.ParseExact("18:00", "HH:mm", enUS))
                {
                    this.TimeOfVisit = value;
                }
            } 
        }
    }
}
