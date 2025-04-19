using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Services.Implementations;
using Clinic_Management_system.Services.Interfaces;

namespace Clinic_Management_system.UI.View
{
    public class DoctorView
    {
        private readonly IDoctorService _doctorService;
        private readonly IUserService _userService;

        public DoctorView(IDoctorService doctorService, IUserService userService)
        {
            _doctorService = doctorService;
            _userService = userService;
        }

        public void AddDoctor()
        {
            string name = InputHelper.GetName("Doctor");
            string email = InputHelper.GetEmail("Doctor");

            while (_userService.IsEmailExist(email))
            {
                ConsoleHelper.PrintMessage($"Email '{email}' is already exist. Please use a different email.\n", false);
                email = InputHelper.GetEmail("Doctor");
            }

            string phoneNumber = InputHelper.GetPhoneNumber("Doctor");
            Specialty specialty = InputHelper.GetSpecialty();

            Doctor doctor = new Doctor()
            {
                DoctorName = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Specialty = specialty,
            };

            (bool isSuccess, string message) = _doctorService.Add(doctor);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
            MakeAccount(name, email, RoleType.Doctor);
        }

        public void UpdateDoctor()
        {
            int id = InputHelper.GetId("Doctor");
            if (_doctorService.IsDoctorExist(id))
            {
                ConsoleHelper.PrintMessage($"\nDoctor With ID '{id}' Not Found, try again", false);
                ConsoleHelper.PressEnterToContinue();
                return;
            }

            Doctor doctor = new Doctor()
            {
                DoctorName = InputHelper.GetName("Doctor"),
                PhoneNumber = InputHelper.GetPhoneNumber("Doctor"),
                Specialty = InputHelper.GetSpecialty()
            };

            (bool isSuccess, string message) = _doctorService.Update(id, doctor);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void RemoveDoctor()
        {
            int id = InputHelper.GetId("Doctor");
            if (InputHelper.ConfirmAction("Remove Doctor"))
            {
                (bool isSuccess, string message) = _doctorService.Delete(id);
                ConsoleHelper.PrintMessage(message, isSuccess);
            }
            else
                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
            ConsoleHelper.PressEnterToContinue();
        }

        public void RemoveAllDoctor()
        {
            if (InputHelper.ConfirmAction("Remove All Doctor"))
            {
                (bool isSuccess, string message) = _doctorService.Clear();
                ConsoleHelper.PrintMessage(message, isSuccess);
            }
            else
                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetDoctorById()
        {
            int id = InputHelper.GetId("Doctor");
            (bool isSuccess, string message) = _doctorService.GetById(id);
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetAllDoctor()
        {
            (bool isSuccess, string message) = _doctorService.GetAll();
            ConsoleHelper.PrintMessage(message, isSuccess);
            ConsoleHelper.PressEnterToContinue();
        }

        public void MakeReport()
        {
            string report = _doctorService.MakeReport();
            ConsoleHelper.PrintMessage(report);
            ConsoleHelper.PressEnterToContinue();
        }

        public void GetDoctorAccountInfo()
        {
            int userId = SessionService.CurrentUser?.UserId ?? 0;
            if (userId == 0)
            {
                ConsoleHelper.PrintMessage("No Docotr with ID '0'", false);
                ConsoleHelper.PressEnterToContinue();
            }
            (bool isSuccess, string message) = _doctorService.GetDoctorAccountInfo(userId);

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