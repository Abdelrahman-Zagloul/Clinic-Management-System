using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Services.Implementations;

namespace Clinic_Management_system.UI.View
{
    public class AuthenticationView
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationView(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public bool Login(RoleType role)
        {
            int times = 3;
            while (times > 0)
            {
                Console.Write("Enter Your Email: ");
                string email = Console.ReadLine() ?? "";
                bool isValidEmail = ValidationHelper.IsValidEmail(email);

                Console.Write("Enter Your Password: ");
                string password = Console.ReadLine() ?? "";
                bool isValidPassword = ValidationHelper.IsValidPassword(password);

                if (isValidPassword && isValidEmail)
                {
                    (bool isSuccess, string message) = _authenticationService.Login(email, password, role);
                    ConsoleHelper.PrintMessage(message, isSuccess);

                    if (isSuccess)
                        return true;
                    else
                        times--;
                }
                else
                {
                    ConsoleHelper.PrintMessage("\nInvalid Email or Password format, please try again\n", false);
                    times--;
                }
            }
            ConsoleHelper.PrintMessage("\nToo many failed attempts. Login failed.\n", false);
            ConsoleHelper.PressEnterToContinue();
            Environment.Exit(0);
            return false;
        }

        public void Register(RoleType role)
        {
            Console.Write("Enter Your Name: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Enter Your Email: ");
            string email = Console.ReadLine() ?? "";
            bool isValidEmail = ValidationHelper.IsValidEmail(email);

            Console.Write("Enter Your Password: ");
            string password = Console.ReadLine() ?? "";
            bool isValidPassword = ValidationHelper.IsValidPassword(password);

            if (!(isValidEmail && isValidPassword))
            {
                ConsoleHelper.PrintMessage("\nInvalid Email or Password format, please try again", false);
                ConsoleHelper.PressEnterToContinue();
                return;
            }

            User newUser = new User { Name = name, Email = email, Password = password, Role = role };
            string state = _authenticationService.Register(newUser);

            bool isSuccess = state.Contains("Successfully");
            ConsoleHelper.PrintMessage(state, isSuccess);
        }

        public void ChangePassword(int id, string entity)
        {
            Console.Write("Enter Your Current Password: ");
            string currentPassword = Console.ReadLine() ?? "";
            string newPassword = InputHelper.GetPassword(entity);

            (bool isSuccess, string message) = _authenticationService.ChangePassword(id, currentPassword, newPassword, entity);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }
    }
}