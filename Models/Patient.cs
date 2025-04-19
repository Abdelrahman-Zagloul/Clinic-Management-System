using Clinic_Management_system.Enums;

namespace Clinic_Management_system.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public int ReceptionistId { get; set; }
        public Receptionist Receptionist { get; set; }

        public override string ToString()
        {
            return $"\n------ Patient Information -------\n\n" +
                   $"Patient ID     : {PatientId}\n" +
                   $"Name           : {Name}\n" +
                   $"Age            : {Age}\n" +
                   $"Phone          : {Phone}\n" +
                   $"Gender         : {Gender}\n" +
                   $"Appointments   : {Appointments?.Count ?? 0}\n" +
                   $"Receptionist   : {(Receptionist != null ? Receptionist.Name : "N/A")}\n\n" +
                   $"---------------------------------------------";
        }
    }
}