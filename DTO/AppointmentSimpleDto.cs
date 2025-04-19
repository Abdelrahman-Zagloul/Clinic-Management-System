using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class AppointmentSimpleDto
    {
        public string PatientName { get; set; } = string.Empty;
        public string ReceptionistName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return
                $"Receptionist Name: {ReceptionistName}\n" +
                $"Date: {Date:dddd, MMMM dd, yyyy}\n" +
                $"Time: {StartTime:hh\\:mm} - {EndTime:hh\\:mm}\n" +
                $"Patient Name: {PatientName}\n" +
                $"Price: {Price:C}";
        }
    }
}