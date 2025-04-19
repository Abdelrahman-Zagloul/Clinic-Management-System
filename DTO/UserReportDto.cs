using Clinic_Management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class UserReportDto
    {
        public int AllUserCount { get; set; }
        public Dictionary<RoleType, int> UserRoles { get; set; } = new();
        public DateTime ReportDate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n====== Users Report ======\n");
            sb.AppendLine($"Report Date: {ReportDate}");
            sb.AppendLine($"Total User: {AllUserCount}");

            foreach (var kvp in UserRoles)
            {
                sb.AppendLine($"- {kvp.Key}: {kvp.Value}");
            }

            sb.AppendLine("\n==========================");
            return sb.ToString();
        }
    }
}