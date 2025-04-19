using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_system.Data.Configuration
{
    public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            builder.ToTable("Receptionists");

            builder.HasKey(x => x.ReceptionistId);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Email)
               .HasMaxLength(200)
               .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.Age)
                .HasColumnType("int");

            builder.Property(x => x.Gender)
                .HasMaxLength(10)
                .HasColumnType("VARCHAR(10)")
                .HasConversion
                (
                x => x.ToString(),
                x => (Gender)Enum.Parse(typeof(Gender), x)
                );

            builder.Property(x => x.ReceptionistShift)
               .HasMaxLength(10)
               .HasColumnType("VARCHAR(10)")
               .HasConversion
               (
               x => x.ToString(),
               x => (Shift)Enum.Parse(typeof(Shift), x)
               );

            builder.Property(x => x.PhoneNumber)
                .HasColumnType("VARCHAR(20)");

            builder.HasIndex(x => x.Name);
        }
    }
}