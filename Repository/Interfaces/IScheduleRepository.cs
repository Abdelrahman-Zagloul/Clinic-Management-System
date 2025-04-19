using Clinic_Management_system.Models;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IScheduleRepository : ICRUDRepository<Schedule>
    {
        bool HasSchedule(int doctorId);

        int GetScheduleId(int doctorId);
    }
}