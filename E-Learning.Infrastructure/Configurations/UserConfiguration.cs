using E_Learning.Domain.Roles;
using E_Learning.Domain.Students;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FullName)
                .HasMaxLength(100)
                .HasConversion(fullname => fullname.Value, value => new FullName(value))
                .IsRequired();

            builder.Property(user => user.Email)
                .HasMaxLength(150)
                .HasConversion(email => email.Value, value => new Email(value))
                .IsRequired();

            builder.Property(user => user.Password)
                .HasMaxLength(255)
                .HasConversion(password => password.Value, value => new Password(value))
                .IsRequired();

            builder.Property(user => user.Address)
                .HasMaxLength(255)
                .HasConversion(address => address.Value, value => new Address(value))
                .IsRequired();

            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(20)
                .HasConversion(phone => phone.Value, value => new PhoneNumber(value))
                .IsRequired();

            builder.Property(user => user.ImageUrl)
                .HasMaxLength(500)
                .HasConversion(img => img != null ? img.Value : null, v => v != null ? new ImageUrl(v) : null)
                .IsRequired(false);

            builder.Property(user => user.CreatedAt)
                .IsRequired();

            builder.HasIndex(user => user.Email)
                .IsUnique();

            builder.HasOne(user => user.Role) 
                   .WithMany(role => role.Users)
                   .HasForeignKey(user => user.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}