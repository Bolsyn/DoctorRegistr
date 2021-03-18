using DoctorRegistr.Blank;
using DoctorRegistr.Data;
using DoctorRegistr.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DoctorRegistr
{
    class Program
    {
        static void Main(string[] args)
        {
            InitConfiguration();


            CultureInfo enUS = new CultureInfo("en-US");

            var doctors = new List<Doctor>
            {
                new Doctor
                { 
                    FullName = "Ivan Ivanov",
                    SpecialId = Guid.Parse("6E5D08FA-9E66-4639-9742-06F9DF4197A7"),
                    ScheduleId = Guid.Parse("A30B3F5A-7744-4B6D-B99A-1D15848A5B3A"),
                },
                new Doctor
                {
                    FullName = "Boris Borisov",
                    SpecialId = Guid.Parse("627CEDC2-530D-4B35-9BEB-08E3B7E07F79"),
                    ScheduleId = Guid.Parse("37D70B5B-4F33-4D07-B834-211C40C3C5EE")
                }
            };

            var patients = new List<Patient>
            {
                 new Patient
                {
                    FullName = "Ivan Ivanov",
                    TimeOfVisitId = Guid.Parse("8E070ED1-E5B1-4BC5-8176-39B4C53FBD37")
                },
                new Patient
                {
                    FullName = "Boris Borisov",
                    TimeOfVisitId = Guid.Parse("D8E21FB5-EBF6-45AD-AC09-59123ED3389B")
                }
            };

            var visits = new List<TimesToVisits>
            {
                new TimesToVisits
                {
                    Id = Guid.Parse("D8E21FB5-EBF6-45AD-AC09-59123ED3389B"),
                    TimeOfVisit = DateTime.ParseExact("11:00", "HH:mm", enUS )
                },
                new TimesToVisits
                {
                    Id = Guid.Parse("8E070ED1-E5B1-4BC5-8176-39B4C53FBD37"),
                    TimeOfVisit = DateTime.ParseExact("15:00", "HH:mm", enUS )
                },
            };

            var scheduls = new List<Schedule>
            {
                new Schedule
                {
                    Id = Guid.Parse("A30B3F5A-7744-4B6D-B99A-1D15848A5B3A"),
                    TimesOfVisitsId = new List<Guid>
                    {
                        Guid.Parse("D8E21FB5-EBF6-45AD-AC09-59123ED3389B")
                    }

                },
                new Schedule
                {
                    Id = Guid.Parse("37D70B5B-4F33-4D07-B834-211C40C3C5EE"),
                    TimesOfVisitsId = new List<Guid>
                    {
                        Guid.Parse("8E070ED1-E5B1-4BC5-8176-39B4C53FBD37")
                    }
                }
            };

            var therapist = new Special 
            {
                Id = Guid.Parse("6E5D08FA-9E66-4639-9742-06F9DF4197A7"),
                Name = "Терапевт" 
            };
            var surgeon = new Special 
            {
                Id = Guid.Parse("627CEDC2-530D-4B35-9BEB-08E3B7E07F79"),
                Name = "Хирург" 
            };


        }


        private static void InitConfiguration()
        {
            ConfigurationService.Init();
        }
    }
}
