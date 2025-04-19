using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Services.Interfaces;

namespace Clinic_Management_system.UI.View
{
    public class UserView
    {
        private readonly IUserService _userService;

        public UserView(IUserService userService)
        {
            _userService = userService;
        }

        public void AddUser()
        {
            User user = new User()
            {
                Name = InputHelper.GetName("User"),
                Email = InputHelper.GetEmail("User"),
                Password = InputHelper.GetPassword("User"),
                Role = InputHelper.GetRoleType()
            };

            (bool isSuccess, string message) = _userService.Add(user);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void UpdateUser()
        {
            int id = InputHelper.GetId("User");
            if (_userService.IsUserExist(id))
            {
                ConsoleHelper.PrintMessage($"\nUser with ID '{id}' not found. Please try again.", false);
                ConsoleHelper.PressEnterToContinue();
                return;
            }

            User user = new User()
            {
                Name = InputHelper.GetName("User"),
                Password = InputHelper.GetPassword("User"),
                Role = InputHelper.GetRoleType()
            };

            (bool isSuccess, string message) = _userService.Update(id, user);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void RemoveUser()
        {
            int id = InputHelper.GetId("User");
            if (InputHelper.ConfirmAction("Remove User"))
            {
                (bool isSuccess, string message) = _userService.Delete(id);
                ConsoleHelper.PrintMessage(message, isSuccess);
            }
            else
                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetUserById()
        {
            int id = InputHelper.GetId("User");
            (bool isSuccess, string message) = _userService.GetById(id);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAccountInfo(int id)
        {
            (bool isSuccess, string message) = _userService.GetById(id);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAllUser()
        {
            (bool isSuccess, string message) = _userService.GetAll();
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void RemoveAllUser()
        {
            if (InputHelper.ConfirmAction("Remove All User"))
            {
                (bool isSuccess, string message) = _userService.Clear();
                ConsoleHelper.PrintMessage(message, isSuccess);
            }
            else
                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
            ConsoleHelper.PressEnterToContinue();
        }

        public void MakeReport()
        {
            string report = _userService.MakeReport();
            ConsoleHelper.PrintMessage(report);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GenerateManagerReport()
        {
            string report = _userService.GenerateManagerReport();
            ConsoleHelper.PrintMessage(report);
            ConsoleHelper.PressEnterToContinue();
        }
    }
}