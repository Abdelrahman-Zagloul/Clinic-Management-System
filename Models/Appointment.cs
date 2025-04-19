using Clinic_Management_system.Enums;

namespace Clinic_Management_system.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public ConsultationType ConsultationType { get; set; }
        public decimal Price { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int DurationInMinutes => (int)(EndTime - StartTime).TotalMinutes;
        public string Notes { get; set; } = string.Empty;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public override string ToString()
        {
            return $"\n------ Appointment Information -------\n\n" +
                   $"Appointment ID  : {AppointmentId}\n" +
                   $"Date            : {Date:yyyy-MM-dd}\n" +
                   $"Time            : {StartTime:hh\\:mm} - {EndTime:hh\\:mm} ({DurationInMinutes} mins)\n" +
                   $"Type            : {ConsultationType}\n" +
                   $"Price           : {Price:C}\n" +
                   $"Doctor Name     : {Doctor?.DoctorName ?? "N/A"} (ID: {DoctorId})\n" +
                   $"Patient Name    : {Patient?.Name ?? "N/A"} (ID: {PatientId})\n" +
                   $"Notes           : {(string.IsNullOrWhiteSpace(Notes) ? "None" : Notes)}\n\n" +
                   $"----------------------------------------------";
        }
    }
}