using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_system.Data.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(x => x.DoctorId);

            builder.Property(x => x.Email)
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.DoctorName)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.PhoneNumber)
              .HasColumnType("VARCHAR(20)");

            builder.Property(x => x.Specialty)
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .HasConversion
                (
                   x => x.ToString(),
                   x => (Specialty)Enum.Parse(typeof(Specialty), x)
                );
        }
    }
}