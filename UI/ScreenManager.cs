using Clinic_Management_system.Data;
using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;
using Clinic_Management_system.Models;
using Clinic_Management_system.Repository.Implementations;
using Clinic_Management_system.Repository.Interfaces;
using Clinic_Management_system.Services.Implementations;
using Clinic_Management_system.Services.Interfaces;
using Clinic_Management_system.UI.View;
using System.Net.Mail;

namespace Clinic_Management_system.UI
{
    public static class ScreenManager
    {
        private static readonly AppDbContext context = new AppDbContext();

        private static readonly IUserRepository userRepository = new UserRepository(context);
        private static readonly IUserService userService = new UserService(userRepository);
        private static readonly UserView userView = new UserView(userService);

        private static readonly IDoctorRepository doctorRepository = new DoctorRepository(context);
        private static readonly IDoctorService doctorService = new DoctorService(doctorRepository);
        private static readonly DoctorView doctorView = new DoctorView(doctorService, userService);

        private static readonly IReceptionistRepository receptionistRepository = new ReceptionistRepository(context);
        private static readonly IReceptionistService receptionistService = new ReceptionistService(receptionistRepository);
        private static readonly ReceptionistView receptionistView = new ReceptionistView(receptionistService, userService);

        private static readonly IScheduleRepository scheduleRepository = new ScheduleRepository(context);
        private static readonly IScheduleService scheduleService = new ScheduleService(scheduleRepository);
        private static readonly ScheduleView scheduleView = new ScheduleView(scheduleService, doctorService);

        private static readonly PatientRepository patientRepository = new PatientRepository(context);
        private static readonly PatientService patientService = new PatientService(patientRepository);

        private static readonly AuthenticationRepository authenticationRepository = new AuthenticationRepository(context);
        private static readonly AuthenticationService authenticationService = new AuthenticationService(authenticationRepository);
        private static readonly AuthenticationView authenticationView = new AuthenticationView(authenticationService);

        private static readonly AppointmentRepository appointmentRepository = new AppointmentRepository(context);
        private static readonly AppointmentService appointmentService = new AppointmentService(appointmentRepository);
        private static readonly AppointmentView appointmentView = new AppointmentView(appointmentService, patientRepository, receptionistService, doctorService, appointmentRepository, patientService);

        public static void Run()
        {
            while (true)
            {
                byte choice = Screens.ShowMainMenu();
                switch (choice)
                {
                    case 1:
                        Screen_Manager();
                        break;

                    case 2:
                        Screen_Doctor();
                        break;

                    case 3:
                        Screen_Receptionist();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        //--------------------------111--------------------------
        private static void Screen_Manager()
        {
            while (true)
            {
                byte choice = Screens.ShowLoginAndRegisterMenu("Manager");
                switch (choice)
                {
                    case 1: // Manager Screen Login
                        Handle_ManagerLogin();
                        break;

                    case 2:// Manager Screen Register
                        ConsoleHelper.PrintMessage("\nYou can't create an account. Only an existing Manager can create accounts.", false);
                        ConsoleHelper.PressEnterToContinue();
                        break;

                    case 3: // Return To Main Screen
                        return;

                    case 4: // End Program
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        private static void Screen_Doctor()
        {
            while (true)
            {
                byte choice = Screens.ShowLoginAndRegisterMenu("Doctor");
                switch (choice)
                {
                    case 1: // Doctor Screen Login
                        Handle_DoctorLogin();
                        break;

                    case 2: // Doctor Screen Register
                        ConsoleHelper.PrintMessage("\nYou can't create an account. Only a Manager can create Doctor accounts.", false);
                        ConsoleHelper.PressEnterToContinue();
                        break;

                    case 3:// Return To Main Screen
                        return;

                    case 4:// End Program
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        private static void Screen_Receptionist()
        {
            while (true)
            {
                byte choice = Screens.ShowLoginAndRegisterMenu("Receptionist");
                switch (choice)
                {
                    case 1:// Receptionist Screen Login
                        Handle_ReceptionistLogin();
                        break;

                    case 2:// Receptionist Screen Register
                        ConsoleHelper.PrintMessage("\nYou can't create an account. Only a Manager can create Receptionist accounts.", false);
                        ConsoleHelper.PressEnterToContinue();
                        break;

                    case 3:// Return To Main Screen
                        return;

                    case 4:// End Program
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        //--------------------------111--------------------------

        //--------------------------222--------------------------
        private static void Handle_ManagerLogin()
        {
            bool loginSuccess = authenticationView.Login(RoleType.Manager);
            ConsoleHelper.PressEnterToContinue();
            if (loginSuccess)
            {
                while (true)
                {
                    byte choice = Screens.ShowManagerOperationsMenu();
                    switch (choice)
                    {
                        case 1:
                            HandelManager_UserManager();
                            break;

                        case 2:
                            HandelManager_DoctorManager();
                            break;

                        case 3:
                            HandelManager_ReceptionistManager();
                            break;

                        case 4:
                            userView.GenerateManagerReport();
                            break;

                        case 5: // Return To Login & Register Menu
                            SessionService.Logout();
                            return;

                        case 6: // End Program
                            Environment.Exit(0);
                            break;

                        default:
                            ConsoleHelper.DefaultErorrMessage();
                            ConsoleHelper.PressEnterToContinue();
                            break;
                    }
                }
            }
        }

        private static void Handle_DoctorLogin()
        {
            bool loginSuccess = authenticationView.Login(RoleType.Doctor);
            ConsoleHelper.PressEnterToContinue();
            if (loginSuccess)
            {
                while (true)
                {
                    byte choice = Screens.ShowDoctorOperationsMenu();
                    switch (choice)
                    {
                        case 1:
                            appointmentView.GetTodayAppointmentsByDoctor();
                            break;

                        case 2:
                            appointmentView.GetAppointmentsByDoctor();
                            break;

                        case 3:
                            HandelDoctor_ScheduleManager();
                            break;

                        case 4:
                            authenticationView.ChangePassword(SessionService.CurrentUser?.UserId ?? 0, "Doctor");
                            break;

                        case 5:
                            doctorView.GetDoctorAccountInfo();
                            break;

                        case 6: // Return To Login & Register Menu
                            SessionService.Logout();
                            return;

                        case 7: // End Program
                            Environment.Exit(0);
                            break;

                        default:
                            ConsoleHelper.DefaultErorrMessage();
                            ConsoleHelper.PressEnterToContinue();
                            break;
                    }
                }
            }
        }

        private static void Handle_ReceptionistLogin()
        {
            bool loginSuccess = authenticationView.Login(RoleType.Receptionist);
            ConsoleHelper.PressEnterToContinue();
            if (loginSuccess)
            {
                while (true)
                {
                    byte choice = Screens.ShowReceptionistOperationsMenu();
                    switch (choice)
                    {
                        case 1:
                            appointmentView.AddAppointment();
                            break;

                        case 2:
                            appointmentView.SearchByName();
                            break;

                        case 3:
                            if (InputHelper.ConfirmAction("Remove Appointment"))
                                appointmentView.DeleteAppointment();
                            else
                            {
                                ConsoleHelper.PrintMessage("\nOperation canceled.", true);
                                ConsoleHelper.PressEnterToContinue();
                            }
                            break;

                        case 4:
                            authenticationView.ChangePassword(SessionService.CurrentUser?.UserId ?? 0, "Receptionist");
                            break;

                        case 5:
                            receptionistView.GetReceptionistAccountInfo();
                            break;

                        case 6: // Return To Login & Register Menu
                            SessionService.Logout();
                            return;

                        case 7: // End Program
                            Environment.Exit(0);
                            break;

                        default:
                            ConsoleHelper.DefaultErorrMessage();
                            break;
                    }
                }
            }
        }

        //--------------------------222--------------------------

        //--------------------------333--------------------------
        private static void HandelManager_UserManager()
        {
            while (true)
            {
                byte choice = Screens.ShowManageUserMenu();
                switch (choice)
                {
                    case 1: // Add User
                        userView.AddUser();
                        break;

                    case 2:// Update User
                        userView.UpdateUser();
                        break;

                    case 3: //Remove User
                        userView.RemoveUser();
                        break;

                    case 4: //Get User by ID
                        userView.GetUserById();
                        break;

                    case 5: // Get All User
                        userView.GetAllUser();
                        break;

                    case 6://Remove All User
                        userView.RemoveAllUser();
                        break;

                    case 7: // Make Report
                        userView.MakeReport();
                        break;

                    case 8: // Return To Login & Register Menu
                        SessionService.Logout();
                        return;

                    case 9: // End Program
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        private static void HandelManager_DoctorManager()
        {
            while (true)
            {
                byte choice = Screens.ShowManageDoctorMenu();
                switch (choice)
                {
                    case 1:
                        doctorView.AddDoctor();
                        break;

                    case 2:
                        doctorView.UpdateDoctor();
                        break;

                    case 3:
                        doctorView.RemoveDoctor();
                        break;

                    case 4:
                        doctorView.GetDoctorById();
                        break;

                    case 5:
                        doctorView.GetAllDoctor();
                        break;

                    case 6:
                        doctorView.RemoveAllDoctor();
                        break;

                    case 7:
                        doctorView.MakeReport();
                        break;

                    case 8: // Return To Login & Register Menu
                        SessionService.Logout();
                        return;

                    case 9: // End Program
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        private static void HandelManager_ReceptionistManager()
        {
            while (true)
            {
                byte choice = Screens.ShowManageReceptionistMenu();
                switch (choice)
                {
                    case 1:
                        receptionistView.AddReceptionist();
                        break;

                    case 2:
                        receptionistView.UpdateReceptionist();
                        break;

                    case 3:

                        receptionistView.RemoveReceptionist();

                        break;

                    case 4:
                        receptionistView.GetReceptionistByID();

                        break;

                    case 5:
                        receptionistView.GetAllReceptionist();
                        break;

                    case 6:
                        receptionistView.RemoveAllReceptionist();
                        break;

                    case 7:
                        receptionistView.MakeReport();
                        break;

                    case 8: // Return To Login & Register Menu
                        SessionService.Logout();
                        return;

                    case 9: // End Program
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }

        //--------------------------333--------------------------

        private static void HandelDoctor_ScheduleManager()
        {
            while (true)
            {
                byte choice = Screens.ShowDoctorScheduleMenu();
                switch (choice)
                {
                    case 1:
                        scheduleView.AddSchedule();
                        break;

                    case 2:
                        scheduleView.UpdateSchedule();
                        break;

                    case 3:
                        if (InputHelper.ConfirmAction("Remove Schedule"))
                            scheduleView.RemoveSchedule();
                        else
                        {
                            ConsoleHelper.PrintMessage("\nOperation canceled.", true);
                            ConsoleHelper.PressEnterToContinue();
                        }
                        break;

                    case 4:
                        scheduleView.ShowScheduleInformation();
                        break;

                    case 5:
                        return;

                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }
    }
}