using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;

namespace Clinic_Management_system.Repository.Interfaces
{
    public interface IAuthenticationRepository
    {
        User? Login(string email, string password, RoleType roleType);

        int Register(User user);

        bool ChangePassword(int userId, string currentPassword, string newPassword, string entity);
    }
}