using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IScheduleService : ICRUDService<Schedule>
    {
        bool HasSchedule(int doctorId);

        int GetScheduleId(int doctorId);
    }
}