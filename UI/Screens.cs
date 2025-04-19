using Clinic_Management_system.Helper;

namespace Clinic_Management_system.UI
{
    public static class Screens
    {
        public static byte ShowMainMenu()
        {
            return DisplayMenu(
                "Main Menu",
                new string[]
                {
                    "[1] Manager",
                    "[2] Doctor",
                    "[3] Receptions",
                    "[4] Exit"
                },
                4);
        }

        public static byte ShowLoginAndRegisterMenu(string role)
        {
            return DisplayMenu(
                role,
                new string[]
                {
                    "[1] Login",
                    "[2] Register",
                    "[3] Back",
                    "[4] Exit"
                },
                4);
        }

        #region Manager Screens

        public static byte ShowManagerOperationsMenu()
        {
            return DisplayMenu(
                "Manager Operation",
                new string[]
                {
                    "[1] Manage User",
                    "[2] Manage Doctor",
                    "[3] Manage Receptionist",
                    "[4] Make Report",
                    "[5] Back",
                    "[6] Exit"
                },
                6);
        }

        public static byte ShowManageReceptionistMenu()
        {
            return DisplayMenu(
                "Receptionist Operation",
                new string[]
                {
                    "[1] Add Receptionist",
                    "[2] Update Receptionist",
                    "[3] Remove Receptionist",
                    "[4] Get Receptionist By ID",
                    "[5] Get All Receptionist",
                    "[6] Remove All Receptionist",
                    "[7] Make Report",
                    "[8] Back",
                    "[9] Exit"
                },
                9);
        }

        public static byte ShowManageDoctorMenu()
        {
            return DisplayMenu(
                "Doctor Operation",
                new string[]
                {
                    "[1] Add Doctor",
                    "[2] Update Doctor",
                    "[3] Remove Doctor",
                    "[4] Get Doctor By ID",
                    "[5] Get All Doctor",
                    "[6] Remove All Doctors",
                    "[7] Make Report ",
                    "[8] Back",
                    "[9] Exit"
                },
                9);
        }

        public static byte ShowManageUserMenu()
        {
            return DisplayMenu(
                "User Operation",
                new string[]
                {
                    "[1] Add User",
                    "[2] Update User",
                    "[3] Remove User",
                    "[4] Get User By ID",
                    "[5] Get All User",
                    "[6] Remove All User",
                    "[7] Make Report",
                    "[8] Back",
                    "[9] Exit"
                },
               9);
        }

        #endregion Manager Screens

        #region Doctor Screens

        public static byte ShowDoctorOperationsMenu()
        {
            return DisplayMenu(
                "Doctor Operations",
                new string[]
                {
                    "[1] Show Today’s Patients",
                    "[2] Show All Patients",
                    "[3] Manage Schedule",
                    "[4] Change Password",
                    "[5] Show Account Info",
                    "[6] Back",
                    "[7] Exit"
                },
                7);
        }

        public static byte ShowDoctorScheduleMenu()
        {
            return DisplayMenu(
                "Schedule Manager",
                new string[]
                {
                    "[1] Add Schedule",
                    "[2] Update Schedule",
                    "[3] Remove Schedule",
                    "[4] Show Schedule Info",
                    "[5] Back",
                    "[6] Exit"
                },
                6);
        }

        #endregion Doctor Screens

        #region ReceptionistScreen

        public static byte ShowReceptionistOperationsMenu()
        {
            return DisplayMenu(
                "Receptionist Operation",
                new string[]
                {
                    "[1] Add Patient",
                    "[2] Search By Name",
                    "[3] Remove Appointment",
                    "[4] Change Password",
                    "[5] Show Account Info",
                    "[6] Back",
                    "[7] Exit"
                },
                7);
        }

        #endregion ReceptionistScreen

        private static byte DisplayMenu(string title, string[] options, byte maxOption)
        {
            Console.Clear();

            int contentWidth = Math.Max(title.Length, options.Max(opt => opt.Length)) + 6;
            string horizontalBorder = new string('═', contentWidth);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n");

            int titlePadding = (contentWidth - title.Length - 2) / 2;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"          ╔{horizontalBorder}╗");
            Console.WriteLine($"          ║{new string(' ', titlePadding)}{title}{new string(' ', contentWidth - title.Length - titlePadding)}║");
            Console.WriteLine($"          ║{new string(' ', contentWidth)}║");

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var option in options)
            {
                Console.WriteLine($"          ║   {option.PadRight(contentWidth - 3)}║");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"          ║{new string(' ', contentWidth)}║");
            Console.WriteLine($"          ╚{horizontalBorder}╝");

            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nPlease choose an option from the menu above: ");

            return ValidationHelper.ValidateInputScreen(Console.ReadLine() ?? "", maxOption);
        }
    }
}