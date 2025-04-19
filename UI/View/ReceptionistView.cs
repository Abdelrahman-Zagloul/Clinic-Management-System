using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Services.Implementations;
using Clinic_Management_system.Services.Interfaces;

namespace Clinic_Management_system.UI.View
{
    public class ReceptionistView
    {
        private readonly IReceptionistService _receptionistService;
        private readonly IUserService _userService;

        public ReceptionistView(IReceptionistService receptionistService, IUserService userService)
        {
            _receptionistService = receptionistService;
            _userService = userService;
        }

        public void AddReceptionist()
        {
            string name = InputHelper.GetName("Receptionist");
            string email = InputHelper.GetEmail("Receptionist");

            while (_userService.IsEmailExist(email))
            {
                ConsoleHelper.PrintMessage($"\nEmail '{email}' is already exist. Please use a different email.\n", false);
                email = InputHelper.GetEmail("Receptionist");
            }

            int age = InputHelper.GetAge("Receptionist", 20, 60);
            Gender gender = InputHelper.GetGender();
            string phoneNumber = InputHelper.GetPhoneNumber("Receptionist");
            Shift shift = InputHelper.GetShift();

            Receptionist receptionist = new Receptionist()
            {
                Name = name,
                Email = email,
                Gender = gender,
                Age = age,
                PhoneNumber = phoneNumber,
                ReceptionistShift = shift
            };

            (bool isSuccess, string message) = _receptionistService.Add(receptionist);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
            MakeAccount(name, email, RoleType.Receptionist);
        }

        public void UpdateReceptionist()
        {
            int id = InputHelper.GetId("Receptionist");
            if (!_receptionistService.IsReceptionistExist(id))
            {
                ConsoleHelper.PrintMessage($"\nReceptionist With ID '{id}' Not Found, try again", false);
                ConsoleHelper.PressEnterToContinue();
                return;
            }

            Receptionist receptionist = new Receptionist()
            {
                Age = InputHelper.GetAge("Receptionist", 20, 60),
                PhoneNumber = InputHelper.GetPhoneNumber("Receptionist"),
                ReceptionistShift = InputHelper.GetShift()
            };

            (bool isSuccess, string message) = _receptionistService.Update(id, receptionist);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void RemoveReceptionist()
        {
            if (InputHelper.ConfirmAction("Remove Receptionist"))
            {
                int id = InputHelper.GetId("Receptionist");
                (bool isSuccess, string message) = _receptionistService.Delete(id);
                ConsoleHelper.PrintMessage(message, isSuccess);
            }
            else
                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
            ConsoleHelper.PressEnterToContinue();
        }

        public void RemoveAllReceptionist()
        {
            if (InputHelper.ConfirmAction("Remove All Receptionist"))
            {
                (bool isSuccess, string message) = _receptionistService.Clear();
                ConsoleHelper.PrintMessage(message, isSuccess);
            }
            else
                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAllReceptionist()
        {
            (bool isSuccess, string message) = _receptionistService.GetAll();
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetReceptionistByID()
        {
            int id = InputHelper.GetId("Receptionist");
            (bool isSuccess, string message) = _receptionistService.GetById(id);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void MakeReport()
        {
            string report = _receptionistService.MakeReport();
            ConsoleHelper.PrintMessage(report);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetReceptionistAccountInfo()
        {
            int userId = SessionService.CurrentUser?.UserId ?? 0;
            if (userId == 0)
            {
                ConsoleHelper.PrintMessage("No Receptionist with ID '0'", false);
                ConsoleHelper.PressEnterToContinue();
            }
            (bool isSuccess, string message) = _receptionistService.GetReceptionistAccountInfo(userId);

            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        private void MakeAccount(string name, string email, RoleType roleType)
        {
            User user = new User
            {
                Name = name,
                Email = email,
                Password = "Test12345",
                Role = roleType,
            };
            _userService.Add(user);
        }
    }
}