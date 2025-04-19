using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_system.Data.Configuration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules");

            builder.HasKey(x => x.ScheduleId);

            builder.Property(x => x.Sunday).HasColumnType("BIT");
            builder.Property(x => x.Monday).HasColumnType("BIT");
            builder.Property(x => x.Tuesday).HasColumnType("BIT");
            builder.Property(x => x.Wednesday).HasColumnType("BIT");
            builder.Property(x => x.Friday).HasColumnType("BIT");
            builder.Property(x => x.Saturday).HasColumnType("BIT");
            builder.Property(x => x.Thursday).HasColumnType("BIT");

            builder.Property(x => x.StartTime)
                .HasColumnType("time");

            builder.Property(x => x.EndTime)
                .HasColumnType("time");

            builder.HasOne(x => x.Doctor)
                .WithOne(x => x.Schedule)
                .HasForeignKey<Schedule>(x => x.DoctorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}