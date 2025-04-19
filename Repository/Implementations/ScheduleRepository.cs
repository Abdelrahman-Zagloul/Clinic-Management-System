using Clinic_Management_system.Data;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Implementations
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;

        public ScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Schedule schedule)
        {
            try
            {
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
                return schedule.ScheduleId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Schedule? GetById(int scheduleId)
        {
            try
            {
                return _context.Schedules.Include(x => x.Doctor).FirstOrDefault(x => x.ScheduleId == scheduleId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Schedule> GetAll()
        {
            //Not Use yet
            try
            {
                return _context.Schedules.Include(x => x.Doctor).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int scheduleId, Schedule newSchedule)
        {
            try
            {
                var schedule = _context.Schedules.FirstOrDefault(x => x.ScheduleId == scheduleId);
                if (schedule == null)
                    return false;

                schedule.Sunday = newSchedule.Sunday;
                schedule.Monday = newSchedule.Monday;
                schedule.Tuesday = newSchedule.Tuesday;
                schedule.Wednesday = newSchedule.Wednesday;
                schedule.Thursday = newSchedule.Thursday;
                schedule.Friday = newSchedule.Friday;
                schedule.Saturday = newSchedule.Saturday;

                schedule.StartTime = newSchedule.StartTime;
                schedule.EndTime = newSchedule.EndTime;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int scheduleId)
        {
            try
            {
                var schedule = _context.Schedules.FirstOrDefault(x => x.ScheduleId == scheduleId);
                if (schedule == null)
                    return false;

                _context.Schedules.Remove(schedule);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Clear()
        {
            // Not use yet
            try
            {
                return _context.Schedules.ExecuteDelete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool HasSchedule(int doctorId)
        {
            try
            {
                return _context.Schedules.AsNoTracking().Any(x => x.DoctorId == doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetScheduleId(int doctorId)
        {
            try
            {
                var schedule = _context.Schedules.AsNoTracking().FirstOrDefault(x => x.DoctorId == doctorId);
                return schedule?.ScheduleId ?? -1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}