using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class ManagerReportDto
    {
        public int AllUserAccount { get; set; }
        public int ManagerAccounts { get; set; }
        public int DoctorAccounts { get; set; }
        public int ReceptionistAccounts { get; set; }

        public int ManagerCount { get; set; }
        public int ReceptionistCount { get; set; }
        public int DoctorCount { get; set; }
        public int PatientCount { get; set; }
        public int ScheduleCount { get; set; }
        public int AppointmentCount { get; set; }
        public DateTime ReportDate { get; set; }

        public override string ToString()
        {
            return $"\n======== Manager Report ========\n\n" +
                   $"Report Date         : {ReportDate:yyyy-MM-dd HH:mm}\n\n" +

                   $"--- Accounts ---\n" +
                   $"All User Accounts     : {AllUserAccount}\n" +
                   $"Manager Accounts      : {ManagerAccounts}\n" +
                   $"Doctor Accounts       : {DoctorAccounts}\n" +
                   $"Receptionist Accounts : {ReceptionistAccounts}\n\n" +

                   $"--- Entities ---\n" +
                   $"Managers            : {ManagerCount}\n" +
                   $"Doctors             : {DoctorCount}\n" +
                   $"Receptionists       : {ReceptionistCount}\n" +
                   $"Patients            : {PatientCount}\n" +
                   $"Schedules           : {ScheduleCount}\n" +
                   $"Appointments        : {AppointmentCount}\n" +

                   $"\n===========================================\n";
        }
    }
}