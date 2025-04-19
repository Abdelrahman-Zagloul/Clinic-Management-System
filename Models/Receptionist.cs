using Clinic_Management_system.Enums;

namespace Clinic_Management_system.Models
{
    public class Receptionist
    {
        public int ReceptionistId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Shift ReceptionistShift { get; set; } //AM or BM or AllDay

        public ICollection<Patient> Patients { get; set; } = new List<Patient>();

        public override string ToString()
        {
            return $"\n------ Receptionist Information -------\n\n" +
                   $"Receptionist ID  : {ReceptionistId}\n" +
                   $"Name             : {Name}\n" +
                   $"Email            : {Email}\n" +
                   $"Age              : {Age}\n" +
                   $"Gender           : {Gender}\n" +
                   $"Phone Number     : {PhoneNumber}\n" +
                   $"Shift            : {ReceptionistShift}\n" +
                   $"Patients Count   : {Patients?.Count ?? 0}\n\n" +
                   $"---------------------------------------------";
        }
    }
}