
using E_Learning.Domain.Roles;
using E_Learning.Domain.Students;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table name
            builder.ToTable("Users");
            // Primary key
            builder.HasKey(user => user.Id);
            // Properties
            builder.Property(user => user.FullName)
                .HasMaxLength(55)
                .HasConversion(fullname => fullname.Value, value => new FullName(value))
                .IsRequired();
            builder.Property(user => user.Email)
                .HasMaxLength(55)
                .HasConversion(email => email.Value, value => new Email(value))
                .IsRequired();
            builder.Property(user => user.Password)
                .HasMaxLength(25)
                .HasConversion(password => password.Value, value => new Password(value))
                .IsRequired();
            builder.Property(user => user.Address)
                .HasConversion(address => address.Value, value => new Address(value))
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.PhoneNumber)
                .HasMaxLength(20)
                .HasConversion(phone => phone.Value, value => new PhoneNumber(value))
                .IsRequired();
            builder.Property(user => user.ImageUrl)
             .HasMaxLength(255)
             .HasConversion(img => img != null ? img.Value : null, v => v != null ? new ImageUrl(v) : null)
             .IsRequired(false);
            builder.Property(user => user.CreatedAt)
                .IsRequired();
            //  has index on email to ensure uniqueness
            builder.HasIndex(user => user.Email)
                .IsUnique();
            // Relationships
            builder.HasOne<Role>()
             .WithMany(role => role.Users)
             .HasForeignKey(user => user.RoleId)
             .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(user => user.Teacher)
                .WithOne()
                .HasForeignKey<Teacher>(teacher => teacher.Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(user => user.Student)
                .WithOne()
                .HasForeignKey<Student>(student => student.Id)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.RefreshTokens)
                .WithOne()
                .HasForeignKey(refreshToken => refreshToken.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.Notification)
                .WithOne()
                .HasForeignKey(notification => notification.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
