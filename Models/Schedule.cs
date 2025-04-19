using System;
using Clinic_Management_system.Enums;

namespace Clinic_Management_system.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public override string ToString()
        {
            var workingDays = new List<string>();

            if (Monday) workingDays.Add("Monday");
            if (Tuesday) workingDays.Add("Tuesday");
            if (Wednesday) workingDays.Add("Wednesday");
            if (Thursday) workingDays.Add("Thursday");
            if (Friday) workingDays.Add("Friday");
            if (Saturday) workingDays.Add("Saturday");
            if (Sunday) workingDays.Add("Sunday");

            string daysString = workingDays.Count > 0
                ? string.Join(", ", workingDays)
                : "No working days";

            return $"\n------ Doctor Schedule -------\n\n" +
                   $"Schedule ID   : {ScheduleId}\n" +
                   $"Doctor Name   : {Doctor?.DoctorName ?? "N/A"} (ID: {DoctorId})\n" +
                   $"Working Days  : {daysString}\n" +
                   $"Hours         : {StartTime:hh\\:mm} - {EndTime:hh\\:mm}\n\n" +
                   $"-------------------------------";
        }
    }
}