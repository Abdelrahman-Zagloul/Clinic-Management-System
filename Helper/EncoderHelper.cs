using System.Text;
using System.Text.RegularExpressions;

namespace Clinic_Management_system.Helper
{
    public static class EncoderHelper
    {
        public static string Encode(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        public static string Decode(string encodedPassword)
        {
            if (!IsBase64String(encodedPassword))
                return "Invalid Base64 input.";

            try
            {
                byte[] bytes = Convert.FromBase64String(encodedPassword);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return "Decoding failed due to invalid format.";
            }
        }

        private static bool IsBase64String(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64))
                return false;

            base64 = base64.Trim();

            return (base64.Length % 4 == 0) &&
                   Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,2}$", RegexOptions.None);
        }
    }
}