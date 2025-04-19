using Clinic_Management_system.DTO;
using Clinic_Management_system.Models;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IAppointmentRepository : ICRUDRepository<Appointment>
    {
        List<Appointment> GetAppointmentsByDoctorId(int doctorId);

        List<Appointment> GetTodayAppointmentsByDoctorId(int doctorId);

        List<Appointment> GetAppointmentsByPatientId(int patientId);

        TimeSpan LastEndTime(int doctorId, DateTime date);

        AppointmentSimpleDto AddAndGetAppointmentSimpleDto(Appointment appointment, int receptionistId);
    }
}