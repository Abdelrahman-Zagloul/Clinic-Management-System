using Clinic_Management_system.Enums;
using Clinic_Management_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinic_Management_system.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Password)
                .HasMaxLength(250)
              .HasColumnType("VARCHAR(250)");

            builder.Property(x => x.Role)
                 .HasMaxLength(50)
                .HasColumnType("VARCHAR(50)")
                .HasConversion
                (
                   x => x.ToString(),
                   x => (RoleType)Enum.Parse(typeof(RoleType), x)
                );
        }
    }
}