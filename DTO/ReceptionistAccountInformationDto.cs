using Clinic_Management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class ReceptionistAccountInformationDto
    {
        public int ReceptionistId { get; set; }
        public int AccountId { get; set; }
        public string ReceptionistName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Shift Shift { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public int NumberOfPatient { get; set; }

        public override string ToString()
        {
            return $"\n\t------ Receptionist Information ------\n\n" +
                   $"Receptionist ID      : {ReceptionistId}\n" +
                   $"Account ID           : {AccountId}\n" +
                   $"Name                 : {ReceptionistName}\n" +
                   $"Email                : {Email}\n" +
                   $"Password             : {Password}\n" +
                   $"Shift                : {Shift}\n" +
                   $"Phone Number         : {PhoneNumber}\n" +
                   $"Age                  : {Age}\n" +
                   $"Gender               : {Gender}\n" +
                   $"Number of Patients   : {NumberOfPatient}\n";
        }
    }
}