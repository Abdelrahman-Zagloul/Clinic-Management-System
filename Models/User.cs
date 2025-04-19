using Clinic_Management_system.Enums;
using Clinic_Management_system.Helper;

namespace Clinic_Management_system.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public RoleType Role { get; set; }

        public override string ToString()
        {
            return $"\n------ User Information -----------\n\n" +
                   $"User ID    : {UserId}\n" +
                   $"Name       : {Name ?? "N/A"}\n" +
                   $"Email      : {Email ?? "N/A"}\n" +
                   $"Password   : {EncoderHelper.Decode(Password) ?? "N/A"}\n" +
                   $"Role       : {Role}\n\n" +
                   $"-----------------------------------------";
        }
    }
}