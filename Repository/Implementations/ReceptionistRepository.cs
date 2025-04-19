using Clinic_Management_system.Data;
using Clinic_Management_system.DTO;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Implementations
{
    public class ReceptionistRepository : IReceptionistRepository
    {
        private readonly AppDbContext _context;

        public ReceptionistRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Receptionist receptionist)
        {
            try
            {
                _context.Receptionists.Add(receptionist);
                _context.SaveChanges();
                return receptionist.ReceptionistId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Update(int receptionistId, Receptionist newReceptionist)
        {
            try
            {
                var receptionist = _context.Receptionists.AsNoTracking().FirstOrDefault(x => x.ReceptionistId == receptionistId);
                if (receptionist == null)
                    throw new InvalidOperationException($"No Receptionist found with ID {receptionistId}");

                receptionist.Name = newReceptionist.Name;
                receptionist.Email = newReceptionist.Email;
                receptionist.Age = newReceptionist.Age;
                receptionist.PhoneNumber = newReceptionist.PhoneNumber;
                receptionist.ReceptionistShift = newReceptionist.ReceptionistShift;
                receptionist.Gender = newReceptionist.Gender;

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(int receptionistId)
        {
            try
            {
                var receptionist = _context.Receptionists.FirstOrDefault(x => x.ReceptionistId == receptionistId);

                if (receptionist == null)
                    throw new InvalidOperationException($"No Receptionist found with ID: {receptionistId}.");

                _context.Receptionists.Remove(receptionist);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Receptionist? GetById(int receptionistId)
        {
            try
            {
                return _context.Receptionists.Include(x => x.Patients).FirstOrDefault(x => x.ReceptionistId == receptionistId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Receptionist> GetAll()
        {
            try
            {
                return _context.Receptionists.Include(x => x.Patients).ToList();
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
                return _context.Receptionists.ExecuteDelete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ReceptionistReportDto MakeReport()
        {
            try
            {
                var receptionistCounts = _context.Receptionists
                  .GroupBy(u => u.ReceptionistShift)
                  .Select(g => new
                  {
                      ReceptionistShift = g.Key,
                      Count = g.Count()
                  })
                  .ToList();

                var report = new ReceptionistReportDto
                {
                    AllReceptionistsCount = _context.Receptionists.Count(),
                    ReportDate = DateTime.Now,
                };

                foreach (var receptionist in receptionistCounts)
                {
                    report.ReceptionistShift.Add(receptionist.ReceptionistShift, receptionist.Count);
                }

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ReceptionistAccountInformationDto GetReceptionistAccountInfo(int receptionistId)
        {
            try
            {
                var accountInfo = _context.Users.AsNoTracking().FirstOrDefault(x => x.UserId == receptionistId);

                if (accountInfo is null)
                    throw new Exception($"No account found with ID: {receptionistId}");

                var receptionistInfo = _context.Receptionists.AsNoTracking().FirstOrDefault(x => x.Email == accountInfo.Email);

                if (receptionistInfo is null)
                    throw new Exception($"No receptionist found with Email: {accountInfo.Email}");

                int numberOfPatients = _context.Patients
                    .Count(x => x.ReceptionistId == receptionistInfo.ReceptionistId);

                return new ReceptionistAccountInformationDto
                {
                    AccountId = accountInfo.UserId,
                    ReceptionistName = accountInfo.Name,
                    Email = accountInfo.Email,
                    Password = EncoderHelper.Decode(accountInfo.Password),
                    PhoneNumber = receptionistInfo.PhoneNumber,
                    ReceptionistId = receptionistInfo.ReceptionistId,
                    Shift = receptionistInfo.ReceptionistShift,
                    NumberOfPatient = numberOfPatients,
                    Age = receptionistInfo.Age,
                    Gender = receptionistInfo.Gender,
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching receptionist info: {ex.Message}");
            }
        }

        public Receptionist? GetByEmail(string email)
        {
            try
            {
                return _context.Receptionists.AsNoTracking().FirstOrDefault(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}