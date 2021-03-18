using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorRegistr.Blank
{
    public class Doctor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public Guid SpecialId { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
