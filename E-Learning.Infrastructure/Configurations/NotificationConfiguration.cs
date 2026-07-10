using E_Learning.Domain.Notification;
using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder. HasKey(n => n.Id);
            builder .ToTable("Notifications");
            builder.Property(notification => notification.Title)
                .HasConversion(title => title.Value , value => new Title(value))
                    .HasMaxLength(200)
                    .IsRequired();
            builder.Property(notification => notification.Message)
                .HasConversion(message => message.Value , value => new Message(value))
                    .HasMaxLength(1000)
                    .IsRequired();
            builder.Property(notification => notification.UrlRedirect)
                .HasConversion(urlRedirect => urlRedirect.Value , value => new UrlRedirect(value))
                .HasMaxLength(500)
                 .IsRequired(false);


            builder.Property(n => n.CreatedAt)
                .IsRequired();


            builder.Property(n => n.IsRead)
                .HasDefaultValue(true)
                .IsRequired();
            builder.HasOne( n => n.User)
                .WithMany(u => u.Notification)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
