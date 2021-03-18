using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorRegistr.Blank
{
    public class Schedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ICollection<Guid> TimesOfVisitsId { get; set; }
    }
}
