using Microsoft.IdentityModel.Tokens;

namespace Clinic_Management_system.Helper
{
    public static class ConsoleHelper
    {
        public static void PrintMessage(string message, bool flag = true)
        {
            if (flag)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void PressEnterToContinue()
        {
            Console.Write($"\nPress Any Key To Continue . . . ");
            Console.ReadKey();
            Console.WriteLine();
        }

        public static void PrintDot(int timer = 5)
        {
            for (var x = 0; x < timer; x++)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
        }

        public static void DefaultErorrMessage()
        {
            PrintMessage("\nInvalid input, Try Again\n", false);
            PressEnterToContinue();
        }
    }
}