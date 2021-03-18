using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorRegistr.Blank
{
    public class Special
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
