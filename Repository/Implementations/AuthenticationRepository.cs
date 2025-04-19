using Clinic_Management_system.Data;
using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Interfaces;

namespace Clinic_Management_system.Repository.Implementations
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AppDbContext _context;

        public AuthenticationRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? Login(string email, string password, RoleType roleType)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password && x.Role == roleType);
        }

        public int Register(User user)
        {
            if (EmailExist(user.Email))
                throw new InvalidOperationException($"Email '{user.Email}' is already registered. Please use a different email.");

            user.Password = EncoderHelper.Encode(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public bool ChangePassword(int userId, string currentPassword, string newPassword, string entity)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

            if (user == null)
                throw new KeyNotFoundException($"{entity} with ID {userId} not found.");

            if (user.Password != EncoderHelper.Encode(currentPassword))
                throw new UnauthorizedAccessException("Current password is Incorrect.");

            user.Password = EncoderHelper.Encode(newPassword);
            _context.SaveChanges();
            return true;
        }

        private bool EmailExist(string email) => _context.Users.Any(x => x.Email == email);
    }
}