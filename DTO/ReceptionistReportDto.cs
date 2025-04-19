using Clinic_Management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class ReceptionistReportDto
    {
        public int AllReceptionistsCount { get; set; }
        public Dictionary<Shift, int> ReceptionistShift { get; set; } = new();
        public DateTime ReportDate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n====== Receptionist Report ======\n");
            sb.AppendLine($"Report Date: {ReportDate}");
            sb.AppendLine($"Total Receptionist: {AllReceptionistsCount}");

            foreach (var kvp in ReceptionistShift)
            {
                sb.AppendLine($"-{kvp.Key} Shift: {kvp.Value}");
            }

            sb.AppendLine("\n==========================");
            return sb.ToString();
        }
    }
}