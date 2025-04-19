using Clinic_Management_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Management_system.Helper
{
    public static class FormatHelper
    {
        public static string FormatAppointmentsTable(List<Appointment> appointments)
        {
            var sb = new StringBuilder();

            sb.AppendLine("\n\t\t------ All Appointments ------\n");
            sb.AppendLine($"{"ID",-4} | {"Date",-12}   | {"Time",-15} | {"Patient Name",-25} | {"Consultation Type",-25} | {"Price",-8}");
            sb.AppendLine(new string('-', 103));

            foreach (var appointment in appointments)
            {
                string timeRange = $"{appointment.StartTime:hh\\:mm} - {appointment.EndTime:hh\\:mm}";

                sb.AppendLine($"{appointment.AppointmentId,-4} | " +
                              $"{appointment.Date:yyyy-MM-dd,-12} | " +
                              $"{timeRange,-15} | " +
                              $"{(appointment.Patient?.Name ?? "N/A"),-25} | " +
                              $"{appointment.ConsultationType,-25} | " +
                              $"{appointment.Price,3:C0}");
            }

            return sb.ToString();
        }

        public static string FormatDoctorsTable(List<Doctor> doctors)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n\t\t---------- All Doctors ----------\n");
            sb.AppendLine($"{"ID",-4} | {"Name",-20} | {"Specialty",-20} | {"Phone",-11} | {"Email",-30} | {"Number of Appointments",-20} | {"Has Schedule",-8}");
            sb.AppendLine(new string('-', 137));

            foreach (var doctor in doctors)
            {
                sb.AppendLine($"{doctor.DoctorId,-4} | " +
                              $"{doctor.DoctorName,-20} | " +
                              $"{doctor.Specialty,-20} | " +
                              $"{doctor.PhoneNumber,-11} | " +
                              $"{doctor.Email,-30} | " +
                              $"{doctor.Appointments?.Count ?? 0,-20}   | " +
                              $"{(doctor.Schedule != null ? "Yes" : "No"),-8}");
            }

            return sb.ToString();
        }

        public static string FormatPatientSearchResults(List<Patient> patients)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n\t\t\t\t------ Search Results ------\n");
            sb.AppendLine($"{"ID",-5} | {"Name",-25} | {"Age",-3} | {"Phone",-11}  | {"Gender",-6} | {"Number Of Appointments",-5} | {"Receptionist Name",-19}");
            sb.AppendLine(new string('-', 108));

            foreach (var patient in patients)
            {
                sb.AppendLine($"{patient.PatientId,-5} | " +
                              $"{patient.Name,-25} | " +
                              $"{patient.Age,-3} | " +
                              $"{patient.Phone,-12} | " +
                              $"{patient.Gender,-6} | " +
                              $"{(patient.Appointments?.Count ?? 0),-21}  | " +
                              $"{patient.Receptionist.Name ?? "NA",-19}");
            }

            return sb.ToString();
        }

        public static string FormatReceptionistDetails(List<Receptionist> receptionists)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n\t\t------ All Receptionists ------\n");
            sb.AppendLine($"{"ID",-5} | {"Name",-25} | {"Age",-3} | {"Gender",-6} | {"Shift",-10} | {"Number Of Patients",-5}");
            sb.AppendLine(new string('-', 82));

            foreach (var receptionist in receptionists)
            {
                string shift = receptionist.ReceptionistShift.ToString();
                sb.AppendLine($"{receptionist.ReceptionistId,-5} | " +
                              $"{receptionist.Name,-25} | " +
                              $"{receptionist.Age,-3} | " +
                              $"{receptionist.Gender,-6} | " +
                              $"{shift,-10} | " +
                              $"{(receptionist.Patients?.Count ?? 0),-5}");
            }

            return sb.ToString();
        }

        public static string FormatUserDetails(List<User> users)
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n\t\t\t------ All Users ------\n");
            sb.AppendLine($"{"ID",-6} | {"Name",-20} | {"Email",-30} | {"Role",-12} |");
            sb.AppendLine(new string('-', 79));

            foreach (var user in users)
            {
                sb.AppendLine($"{user.UserId,-6} | " +
                              $"{(string.IsNullOrEmpty(user.Name) ? "N/A" : user.Name),-20} | " +
                              $"{(string.IsNullOrEmpty(user.Email) ? "N/A" : user.Email),-30} | " +
                              $"{user.Role,-12} |");
            }

            return sb.ToString();
        }
    }
}