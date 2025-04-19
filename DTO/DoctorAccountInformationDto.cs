using Clinic_Management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class DoctorAccountInformationDto
    {
        public int DoctorId { get; set; }
        public int AccountId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Specialty Specialty { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public bool HasSchedule { get; set; }

        public int NumberOfAppointment { get; set; }

        public override string ToString()
        {
            return $"\n\t------ Doctor Information ------\n\n" +
                   $"Doctor ID              : {DoctorId}\n" +
                   $"Account ID             : {AccountId}\n" +
                   $"Name                   : {DoctorName}\n" +
                   $"Email                  : {Email}\n" +
                   $"Password               : {Password}\n" +
                   $"Specialty              : {Specialty}\n" +
                   $"Phone Number           : {PhoneNumber}\n" +
                   $"Has Schedule           : {(HasSchedule ? "Yes" : "No")}\n" +
                   $"Number of Appointments : {NumberOfAppointment}\n";
        }
    }
}