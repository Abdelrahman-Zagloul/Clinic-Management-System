using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;
using System.Text;

namespace Clinic_Management_system.Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public (bool isSuccess, string message) Add(Schedule schedule)
        {
            try
            {
                int scheduleId = _scheduleRepository.Add(schedule);
                return (true, $"\nSchedule With ID: \"{scheduleId}\" added successfully.");
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
                return _scheduleRepository.Clear()
                    ? (true, "\nAll Schedule Removed Successfully")
                    : (false, "\nNo Schedule To Remove");
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
                bool isRemoved = _scheduleRepository.Delete(id);
                return isRemoved
                    ? (true, "\nRemove Successfully")
                    : (false, "\nSchedule not found or could not be removed.");
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
                var schedules = _scheduleRepository.GetAll();
                if (schedules == null || !schedules.Any())
                    return (false, "\nNo schedules found.");

                StringBuilder sb = new StringBuilder();
                foreach (var schedule in schedules)
                {
                    sb.AppendLine(schedule.ToString());
                }
                return (true, sb.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving schedule data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) GetById(int id)
        {
            try
            {
                var schedule = _scheduleRepository.GetById(id);
                if (schedule == null)
                    return (false, $"\nCan't Find schedule with ID: '{id}'");

                return (true, schedule.ToString());
            }
            catch (Exception ex)
            {
                return (false, $"\nError retrieving Schedule data: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Update(int id, Schedule newSchedule)
        {
            try
            {
                return _scheduleRepository.Update(id, newSchedule)
                    ? (true, $"\nSchedule with ID: '{id}' Updated Successfully")
                    : (false, "\nUpdate failed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public bool HasSchedule(int doctorId)
        {
            try
            {
                return _scheduleRepository.HasSchedule(doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"\nError retrieving Schedule data: {ex.Message}");
            }
        }

        public int GetScheduleId(int doctorId)
        {
            try
            {
                int scheduleId = _scheduleRepository.GetScheduleId(doctorId);
                if (scheduleId == -1)
                    throw new Exception("\nThis doctor doesn't have any schedule. First add a schedule then you can update it.");
                return scheduleId;
            }
            catch (Exception ex)
            {
                throw new Exception($"\nError: {ex.Message}");
            }
        }
    }
}