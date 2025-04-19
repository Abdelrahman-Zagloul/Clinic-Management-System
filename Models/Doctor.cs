using Clinic_Management_system.Enums;

namespace Clinic_Management_system.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public Specialty Specialty { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public Schedule Schedule { get; set; }

        public override string ToString()
        {
            return $"\n----------- Doctor Information -----------\n\n" +
            $"Doctor ID           : {DoctorId}\n" +
            $"Name                : {DoctorName}\n" +
            $"Specialty           : {Specialty}\n" +
            $"Phone Number        : {PhoneNumber}\n" +
            $"Email               : {Email}\n" +
            $"Appointments Count  : {Appointments?.Count ?? 0}\n" +
            $"Has Schedule        : {(Schedule != null ? "Yes" : "No")}\n\n" +
            $"------------------------------------------";
        }
    }
}