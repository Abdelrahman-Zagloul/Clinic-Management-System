using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Interfaces
{
    public interface IAuthenticationService
    {
        (bool isSuccess, string message) Login(string email, string password, RoleType roleType);

        string Register(User user);

        (bool isSuccess, string message) ChangePassword(int userId, string currentPassword, string newPassword, string entity);
    }
}