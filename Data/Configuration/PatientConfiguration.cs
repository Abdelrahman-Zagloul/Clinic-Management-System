using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_system.Data.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.PatientId);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Age)
                .HasColumnType("int");

            builder.Property(x => x.Gender)
                .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .HasConversion
                (
                x => x.ToString(),
                x => (Gender)Enum.Parse(typeof(Gender), x)
                );

            builder.Property(x => x.Phone)
                .HasColumnType("VARCHAR(20)");

            builder.HasIndex(x => x.Name);

            builder.HasOne(x => x.Receptionist)
                .WithMany(x => x.Patients)
                .HasForeignKey(x => x.ReceptionistId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}