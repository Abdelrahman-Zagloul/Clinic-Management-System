using Clinic_Management_system.DTO;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Implementations;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;
using System.Text;

namespace Clinic_Management_system.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public (bool isSuccess, string message) Add(Appointment appointment)
        {
            try
            {
                int doctorId = _appointmentRepository.Add(appointment);
                return (true, $"\nAppointment \"{appointment.AppointmentId}\" added successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) AddAngGetAppointmentSimpleDto(Appointment appointment, int receptionistId)
        {
            try
            {
                AppointmentSimpleDto appointmentSimple = _appointmentRepository.AddAndGetAppointmentSimpleDto(appointment, receptionistId);
                return (true, $"\nAppointment \"{appointment.AppointmentId}\" added successfully. With Details : \n\n{appointmentSimple.ToString()}");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Clear()
        {
            try
            {
                return _appointmentRepository.Clear()
                    ? (true, "\nAll Appointment Remove Successfully")
                    : (false, "\nNo Appointment To Remove");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Delete(int id)
        {
            try
            {
                bool isRemoved = _appointmentRepository.Delete(id);

                return isRemoved
                    ? (true, "\nRemove Successfully ")
                    : (false, "\nAppointment not found or could not be removed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAll()
        {
            try
            {
                var appointments = _appointmentRepository.GetAll();

                if (appointments == null || !appointments.Any())
                    return (false, "\nNo appointments found.");

                string formattedTable = FormatHelper.FormatAppointmentsTable(appointments);
                return (true, formattedTable);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving appointment data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                var appointments = _appointmentRepository.GetAppointmentsByDoctorId(doctorId);

                if (appointments == null || !appointments.Any())
                    return (false, "\nNo appointments found for this doctor.");

                var allAppointments = FormatHelper.FormatAppointmentsTable(appointments);
                return (true, allAppointments);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving appointments: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetTodayAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                var appointments = _appointmentRepository.GetTodayAppointmentsByDoctorId(doctorId);

                if (appointments == null || !appointments.Any())
                    return (false, "\nNo appointments found for this doctor.");

                var allAppointments = FormatHelper.FormatAppointmentsTable(appointments);
                return (true, allAppointments);
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving appointments: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetAppointmentsByPatientId(int patientId)
        {
            try
            {
                var appointments = _appointmentRepository.GetAppointmentsByPatientId(patientId);

                if (appointments == null || !appointments.Any())
                    return (false, "\nNo appointments found for this patient.");

                var stringBuilder = new StringBuilder();
                foreach (var appointment in appointments)
                {
                    stringBuilder.AppendLine(appointment.ToString());
                }

                return (true, stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving appointments: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetById(int id)
        {
            try
            {
                var appointment = _appointmentRepository.GetById(id);

                if (appointment == null)
                    return (false, $"\nCan't Find appointment with ID: '{id}'");

                return (true, appointment.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving doctor data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Update(int id, Appointment newAppointment)
        {
            try
            {
                return _appointmentRepository.Update(id, newAppointment)
                    ? (true, $"\nAppointment with ID: '{id}' Updated Successfully")
                    : (false, "\nUpdate failed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }
    }
}