using System.Text.RegularExpressions;

namespace Clinic_Management_system.Helper
{
    public static class ValidationHelper
    {
        public static byte ValidateInputScreen(string input, byte maxOption)
        {
            bool isValid = byte.TryParse(input, out byte result);
            return (isValid && result > maxOption) ? (byte)0 : result;
        }

        public static bool IsValidEmail(string email)
        {
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsValidPassword(string password)
        {
            return password.Length >= 6 &&
                   password.Any(char.IsDigit) &&
                   password.Any(char.IsUpper);
        }

        public static bool IsValidID(string input)
        {
            int id;
            bool isId = int.TryParse(input, out id);
            return isId && id > 0;
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            phoneNumber = phoneNumber.Trim();

            var pattern = @"^(010|011|012|015)[0-9]{8}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}