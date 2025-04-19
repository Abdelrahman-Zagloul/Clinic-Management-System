using Clinic_Management_system.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_system.DTO
{
    [NotMapped]
    public class DoctorSimpleDto
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public Specialty Specialty { get; set; }

        public override string ToString()
        {
            return $"{DoctorId,-5} {DoctorName,-20} {Specialty}";
        }
    }
}