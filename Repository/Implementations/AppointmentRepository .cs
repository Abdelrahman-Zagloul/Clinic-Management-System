using Clinic_Management_system.Data;
using Clinic_Management_system.DTO;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Implementations
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Appointment appointment)
        {
            try
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return appointment.AppointmentId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int appointmentId, Appointment newAppointment)
        {
            try
            {
                var appointment = _context.Appointments.FirstOrDefault(x => x.AppointmentId == appointmentId);
                if (appointment == null)
                    return false;

                appointment.Date = newAppointment.Date;
                appointment.ConsultationType = newAppointment.ConsultationType;
                appointment.Price = newAppointment.Price;
                appointment.StartTime = newAppointment.StartTime;
                appointment.EndTime = newAppointment.EndTime;
                appointment.Notes = newAppointment.Notes;
                appointment.DoctorId = newAppointment.DoctorId;
                appointment.PatientId = newAppointment.PatientId;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int appointmentId)
        {
            try
            {
                var appointment = _context.Appointments.Find(appointmentId);
                if (appointment == null)
                    return false;

                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Appointment? GetById(int appointmentId)
        {
            try
            {
                return _context.Appointments
                     .Include(a => a.Doctor)
                     .Include(a => a.Patient)
                     .FirstOrDefault(a => a.AppointmentId == appointmentId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Appointment> GetAll()
        {
            try
            {
                return _context.Appointments
                     .Include(a => a.Doctor)
                     .Include(a => a.Patient)
                     .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                return _context.Appointments
                    .Include(x => x.Patient)
                    .Where(x => x.DoctorId == doctorId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Appointment> GetTodayAppointmentsByDoctorId(int doctorId)
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                return _context.Appointments
                    .Include(x => x.Patient)
                    .Where(x => x.DoctorId == doctorId && (x.Date >= today && x.Date < tomorrow)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            try
            {
                return _context.Appointments.Where(x => x.PatientId == patientId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Clear()
        {
            try
            {
                return _context.Appointments.ExecuteDelete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TimeSpan LastEndTime(int doctorId, DateTime date)
        {
            try
            {
                var schedule = _context.Schedules.FirstOrDefault(x => x.DoctorId == doctorId);

                if (schedule == null)
                    throw new Exception("\nThis doctor doesn't have a schedule.");

                bool isDateExist = _context.Appointments.Any(x => x.DoctorId == doctorId && x.Date.Date == date.Date);

                if (isDateExist)
                {
                    var endTime = _context.Appointments
                        .Where(x => x.DoctorId == doctorId && x.Date == date)
                        .Max(x => x.EndTime);

                    if (endTime >= schedule.EndTime)
                    {
                        throw new Exception("\nThere is no time available for this day. Try another day.");
                    }

                    return endTime;
                }
                else
                {
                    return schedule.StartTime;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AppointmentSimpleDto AddAndGetAppointmentSimpleDto(Appointment appointment, int Receptionist)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                string patientName = _context.Patients
                    .AsNoTracking()
                    .Where(p => p.PatientId == appointment.PatientId)
                    .Select(p => p.Name)
                    .FirstOrDefault() ?? "NA";

                string receptionistName = _context.Receptionists
                    .FirstOrDefault(x => x.ReceptionistId == Receptionist)?.Name ?? "NA";

                transaction.Commit();

                return new AppointmentSimpleDto
                {
                    PatientName = patientName,
                    Date = appointment.Date,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                    ReceptionistName = receptionistName,
                    Price = appointment.Price
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Failed to add appointment: {ex.Message}");
            }
        }
    }
}