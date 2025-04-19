using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Implementations;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Interfaces;

namespace Clinic_Management_system.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        public (bool isSuccess, string message) ChangePassword(int userId, string currentPassword, string newPassword, string entity)
        {
            try
            {
                if (userId == 0)
                {
                    return (false, $"{entity} with ID {userId} not found");
                }
                bool isChanged = _authenticationRepository.ChangePassword(userId, currentPassword, newPassword, entity);

                return isChanged ? (true, "\nPassword changed successfully.") : (false, "\nPassword change failed.");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public (bool isSuccess, string message) Login(string email, string password, RoleType role)
        {
            try
            {
                string encodedpassword = EncoderHelper.Encode(password);
                User? user = _authenticationRepository.Login(email, encodedpassword, role);
                if (user != null)
                {
                    SessionService.Login(user);
                    return (true, "\nLogin Successfully");
                }
                else
                    return (false, "\nEmail or Password incorrect , try again\n");
            }
            catch (Exception ex)
            {
                return (false, $"\nError: {ex.Message}");
            }
        }

        public string Register(User user)
        {
            try
            {
                int id = _authenticationRepository.Register(user);
                return "\nRegistration Successfully";
            }
            catch (Exception ex)
            {
                return $"\nError: {ex.Message}";
            }
        }
    }
}