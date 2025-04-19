using Clinic_Management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class DoctorReportDto
    {
        public int AllDoctorsCount { get; set; }
        public Dictionary<Specialty, int> DoctorsPerSpecialty { get; set; } = new();
        public DateTime ReportDate { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("\n====== Doctor Report ======\n");
            sb.AppendLine($"Report Date: {ReportDate}");
            sb.AppendLine($"Total Doctors: {AllDoctorsCount}");

            foreach (var kvp in DoctorsPerSpecialty)
            {
                sb.AppendLine($"{kvp.Key}: {kvp.Value}");
            }

            sb.AppendLine("\n==========================");
            return sb.ToString();
        }
    }
}