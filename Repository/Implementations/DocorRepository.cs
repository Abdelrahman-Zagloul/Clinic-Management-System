using Clinic_Management_system.Data;
using Clinic_Management_system.DTO;
using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Implementations
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return doctor.DoctorId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Update(int doctorId, Doctor newDoctor)
        {
            try
            {
                var doctor = _context.Doctors.AsNoTracking().FirstOrDefault(x => x.DoctorId == doctorId);

                if (doctor == null)
                    throw new InvalidOperationException($"No doctor found with ID {doctorId}");

                doctor.DoctorName = newDoctor.DoctorName;
                doctor.Specialty = newDoctor.Specialty;
                doctor.PhoneNumber = newDoctor.PhoneNumber;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int doctorId)
        {
            try
            {
                var doctor = _context.Doctors.FirstOrDefault(x => x.DoctorId == doctorId);

                if (doctor == null)
                    throw new InvalidOperationException($"No doctor found with ID: {doctorId}.");

                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Doctor? GetById(int doctorId)
        {
            try
            {
                return _context.Doctors.Include(x => x.Appointments).Include(x => x.Schedule).FirstOrDefault(x => x.DoctorId == doctorId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Doctor> GetAll()
        {
            try
            {
                return _context.Doctors.Include(x => x.Appointments).Include(x => x.Schedule).ToList();
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
                return _context.Doctors.ExecuteDelete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DoctorReportDto MakeReport()
        {
            try
            {
                var doctorCounts = _context.Doctors
                  .GroupBy(u => u.Specialty)
                  .Select(g => new
                  {
                      Specialty = g.Key,
                      Count = g.Count()
                  })
                  .ToList();

                var report = new DoctorReportDto
                {
                    AllDoctorsCount = _context.Doctors.Count(),
                    ReportDate = DateTime.Now,
                };

                foreach (var doctor in doctorCounts)
                {
                    report.DoctorsPerSpecialty.Add(doctor.Specialty, doctor.Count);
                }

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Doctor? GetByEmail(string email)
        {
            try
            {
                return _context.Doctors.AsNoTracking().FirstOrDefault(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DoctorAccountInformationDto GetDoctorAccountInfo(int doctorId)
        {
            try
            {
                var accountInfo = _context.Users.AsNoTracking().FirstOrDefault(x => x.UserId == doctorId);
                if (accountInfo == null)
                    throw new Exception($"No Account With ID: {doctorId}");

                var doctorInfo = _context.Doctors.FirstOrDefault(x => x.Email == accountInfo.Email);

                if (doctorInfo == null)
                    throw new Exception($"No Doctor With Email: {accountInfo.Email}");

                bool hasSchedule = _context.Schedules.Any(x => x.DoctorId == doctorInfo.DoctorId);
                int numberOfAppointments = _context.Appointments.Count(x => x.DoctorId == doctorInfo.DoctorId);
                DoctorAccountInformationDto informationDto = new DoctorAccountInformationDto
                {
                    AccountId = accountInfo.UserId,
                    DoctorName = accountInfo.Name,
                    Email = accountInfo.Email,
                    Password = EncoderHelper.Decode(accountInfo.Password),
                    PhoneNumber = doctorInfo.PhoneNumber,
                    DoctorId = doctorInfo.DoctorId,
                    Specialty = doctorInfo.Specialty,
                    HasSchedule = hasSchedule,
                    NumberOfAppointment = numberOfAppointments
                };

                return informationDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"ERROR: Failed to get doctor info: {ex.Message}");
            }
        }

        public List<DoctorSimpleDto> GetDoctorsToReceptionist()
        {
            return _context.Doctors
                .AsNoTracking()
                .Select(d => new DoctorSimpleDto
                {
                    DoctorId = d.DoctorId,
                    DoctorName = d.DoctorName,
                    Specialty = d.Specialty
                })
                .ToList();
        }
    }
}