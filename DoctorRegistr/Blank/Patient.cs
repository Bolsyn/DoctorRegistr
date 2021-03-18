using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorRegistr.Blank
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public Guid TimeOfVisitId { get; set; }
    }
}
