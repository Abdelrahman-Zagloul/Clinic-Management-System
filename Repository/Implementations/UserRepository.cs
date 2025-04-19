using Clinic_Management_system.Data;
using Clinic_Management_system.DTO;
using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_system.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Add(User user)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (IsEmailExist(user.Email))
                throw new InvalidOperationException($"\nEmail '{user.Email}' is already Exist. Please use a different email.");
            user.Password = EncoderHelper.Encode(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public bool Update(int userId, User newUser)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            var user = GetById(userId);
            if (user == null)
                throw new InvalidOperationException($"No User found with ID {userId}");
            if (IsEmailExist(newUser.Email))
                throw new InvalidOperationException($"Email '{user.Email}' is already Exist. Please use a different email.");

            user.Name = newUser.Name;
            user.Email = newUser.Email;
            user.Password = newUser.Password;
            user.Role = newUser.Role;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
                if (user == null)
                    throw new InvalidOperationException($"No User found with ID {userId} .");

                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User? GetById(int userId)
        {
            try
            {
                return _context.Users.Find(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return _context.Users.AsNoTracking().ToList();
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
                return _context.Users.ExecuteDelete() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserReportDto MakeReport()
        {
            try
            {
                var userCounts = _context.Users
                    .GroupBy(u => u.Role)
                    .Select(g => new
                    {
                        Role = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                var report = new UserReportDto
                {
                    AllUserCount = _context.Users.Count(),
                    ReportDate = DateTime.Now
                };
                foreach (var user in userCounts)
                {
                    report.UserRoles.Add(user.Role, user.Count);
                }

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public ManagerReportDto GenerateManagerReport()
        {
            var users = _context.Users.ToList();

            return new ManagerReportDto
            {
                AllUserAccount = users.Count,
                ManagerAccounts = users.Count(u => u.Role == RoleType.Manager),
                DoctorAccounts = users.Count(u => u.Role == RoleType.Doctor),
                ReceptionistAccounts = users.Count(u => u.Role == RoleType.Receptionist),
                ManagerCount = users.Count(u => u.Role == RoleType.Manager),
                DoctorCount = _context.Doctors.Count(),
                ReceptionistCount = _context.Receptionists.Count(),
                PatientCount = _context.Patients.Count(),
                ScheduleCount = _context.Schedules.Count(),
                AppointmentCount = _context.Appointments.Count(),

                ReportDate = DateTime.Now
            };
        }
    }
}