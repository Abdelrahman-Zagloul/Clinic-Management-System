using Clinic_Management_system.Models;

namespace Clinic_Management_system.Services.Implementations
{
    public static class SessionService
    {
        public static User? CurrentUser { get; private set; }

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;
    }
}