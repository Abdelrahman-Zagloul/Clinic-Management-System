using Clinic_Management_system.Enums;

namespace Clinic_Management_system.Helper
{
    public static class InputHelper
    {
        public static RoleType GetRoleType()
        {
            while (true)
            {
                Console.Write("Choose Role ( Manager , Doctor , Receptionist ): ");
                string role = Console.ReadLine()?.Trim() ?? "";

                if (Enum.TryParse(role, ignoreCase: true, out RoleType validRole) && Enum.IsDefined(typeof(RoleType), validRole))
                {
                    return validRole;
                }

                ConsoleHelper.PrintMessage("\nInvalid role. Please enter either Manager, Doctor, or Receptionist.\n", false);
            }
        }

        public static Specialty GetSpecialty()
        {
            while (true)
            {
                Console.WriteLine("\nChoose Specialty from the list below:");
                foreach (var value in Enum.GetValues(typeof(Specialty)))
                {
                    Console.WriteLine($"- {value}");
                }

                Console.Write("Enter your choice: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (Enum.TryParse(input, ignoreCase: true, out Specialty specialty) && Enum.IsDefined(typeof(Specialty), specialty))
                {
                    return specialty;
                }

                ConsoleHelper.PrintMessage("\nInvalid Specialty. Please choose from the list above.\n", false);
            }
        }

        public static ConsultationType GetConsultationType()
        {
            while (true)
            {
                Console.WriteLine("Choose Consultation Type:");
                foreach (var value in Enum.GetValues(typeof(ConsultationType)))
                {
                    Console.WriteLine($"- {value}");
                }

                Console.Write("Your choice: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (Enum.TryParse(input, ignoreCase: true, out ConsultationType type) && Enum.IsDefined(typeof(ConsultationType), type))
                    return type;

                ConsoleHelper.PrintMessage("\nInvalid consultation type. Try again.\n", false);
            }
        }

        public static Shift GetShift()
        {
            while (true)
            {
                Console.Write("Choose Shift : ( AM , BM , All Day ) : ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (Enum.TryParse(input, ignoreCase: true, out Shift shift) && Enum.IsDefined(typeof(Shift), shift))
                {
                    return shift;
                }

                ConsoleHelper.PrintMessage("\nInvalid Shift. Please enter a valid Shift.\n", false);
            }
        }

        public static Gender GetGender()
        {
            while (true)
            {
                Console.Write("Choose Gender : ( Male , Female ) : ");
                string input = Console.ReadLine()?.Trim() ?? "";

                if (Enum.TryParse(input, ignoreCase: true, out Gender gender) && Enum.IsDefined(typeof(Gender), gender))
                {
                    return gender;
                }

                ConsoleHelper.PrintMessage("\nInvalid Gender. Please try again.\n", false);
            }
        }

        public static string GetName(string entity)
        {
            while (true)
            {
                Console.Write($"Enter {entity} Name: ");
                string name = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(name))
                {
                    return name;
                }
                ConsoleHelper.PrintMessage($"\nInvalid {entity} Name format, please try again.\n", false);
            }
        }

        public static string GetEmail(string entity)
        {
            while (true)
            {
                Console.Write($"Enter {entity} Email: ");
                string email = Console.ReadLine() ?? "";

                if (ValidationHelper.IsValidEmail(email))
                {
                    return email;
                }
                ConsoleHelper.PrintMessage($"\nInvalid email format, please try again.\n", false);
            }
        }

        public static string GetPassword(string entity)
        {
            while (true)
            {
                Console.Write($"Enter {entity} Password: ");
                string password = Console.ReadLine() ?? "";

                if (ValidationHelper.IsValidPassword(password))
                {
                    return password;
                }
                ConsoleHelper.PrintMessage("\nPassword must be at least 6 characters long, contain a number, and an uppercase letter, Try Again.\n", false);
            }
        }

        public static int GetId(string entity)
        {
            while (true)
            {
                Console.Write($"Enter {entity} Id : ");
                string input = Console.ReadLine() ?? "";

                if (ValidationHelper.IsValidID(input))
                {
                    return int.Parse(input);
                }
                ConsoleHelper.PrintMessage($"\nInvalid {entity} ID ,Try Again\n", false);
            }
        }

        public static string GetPhoneNumber(string entity)
        {
            while (true)
            {
                Console.Write($"Enter {entity} Phone Number: ");
                string phoneNumber = Console.ReadLine() ?? "";

                if (ValidationHelper.IsValidPhoneNumber(phoneNumber))
                {
                    return phoneNumber;
                }
                ConsoleHelper.PrintMessage("\n Invalid Phone number. It must be 11 digits and start with 010, 011, 012, or 015. Try Again.\n", false);
            }
        }

        public static int GetAge(string entity, int min, int max)
        {
            while (true)
            {
                Console.Write($"Enter {entity} Age : ");
                string input = Console.ReadLine() ?? "";
                if (int.TryParse(input, out int age))
                {
                    if (age > min && age <= max)
                        return age;
                }
                ConsoleHelper.PrintMessage($"\nInvalid {entity} Age ,Try Again\n", false);
            }
        }

        public static bool AskForDay(string dayName)
        {
            while (true)
            {
                Console.Write($"Do you work on {dayName}? (y/n): ");
                string input = Console.ReadLine() ?? "".Trim().ToLower();
                if (input == "y") return true;
                else if (input == "n") return false;
                else Console.WriteLine("Please enter 'y' or 'n' only.");
            }
        }

        public static TimeSpan AskForTime(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                if (TimeSpan.TryParse(input, out TimeSpan result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid time format. Please use HH:mm (e.g. 14:30).");
                }
            }
        }

        public static DateTime GetAppointmentDate()
        {
            while (true)
            {
                Console.Write("Enter Appointment Date (yyyy-MM-dd): ");
                string input = Console.ReadLine() ?? "";

                if (DateTime.TryParse(input, out DateTime date))
                {
                    if (date.Date < DateTime.Now.Date)
                    {
                        ConsoleHelper.PrintMessage($"\nDate must be greater than or equal to today ({DateTime.Now:yyyy-MM-dd}). Try again.\n", false);
                        continue;
                    }
                    return date.Date;
                }

                ConsoleHelper.PrintMessage("\nInvalid date format. Try again.\n", false);
            }
        }

        public static decimal GetAppointmentPrice()
        {
            while (true)
            {
                Console.Write("Enter Price: ");
                string input = Console.ReadLine() ?? "";

                if (decimal.TryParse(input, out decimal price) && price > 0)
                    return price;

                ConsoleHelper.PrintMessage("\nInvalid price. Must be a positive number.\n", false);
            }
        }

        public static string GetNotes()
        {
            Console.Write("Enter Notes (optional): ");
            return Console.ReadLine() ?? "";
        }

        public static bool ConfirmAction(string actionName)
        {
            while (true)
            {
                Console.Write($"\nAre you sure you want to {actionName}? (y/n): ");
                string input = Console.ReadLine() ?? "";
                switch (input)
                {
                    case "y":
                        return true;

                    case "n":
                        return false;

                    default:
                        ConsoleHelper.DefaultErorrMessage();
                        break;
                }
            }
        }
    }
}