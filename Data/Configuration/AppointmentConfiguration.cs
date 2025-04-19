using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_system.Data.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(x => x.AppointmentId);

            builder.Property(x => x.Price)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000)
                .HasColumnType("VARCHAR(1000)");

            builder.Property(x => x.Date)
                .HasColumnType("date");

            builder.Property(x => x.StartTime)
              .HasColumnType("time");

            builder.Property(x => x.EndTime)
              .HasColumnType("time");

            builder.Ignore(x => x.DurationInMinutes);

            builder.Property(x => x.ConsultationType)
                 .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .HasConversion
                (
                   x => x.ToString(),
                   x => (ConsultationType)Enum.Parse(typeof(ConsultationType), x)
                );

            builder.HasOne(x => x.Doctor)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.PatientId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}