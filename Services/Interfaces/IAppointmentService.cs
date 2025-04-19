using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IAppointmentService : ICRUDService<Appointment>
    {
        (bool isSuccess, string message) GetAppointmentsByDoctorId(int doctorId);

        (bool isSuccess, string message) GetTodayAppointmentsByDoctorId(int doctorId);

        (bool isSuccess, string message) GetAppointmentsByPatientId(int patientId);

        (bool isSuccess, string message) AddAngGetAppointmentSimpleDto(Appointment appointment, int receptionistId);
    }
}