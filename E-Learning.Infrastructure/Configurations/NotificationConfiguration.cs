using E_Learning.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    .HasMaxLength(50)
                    .IsRequired();
            builder.Property(notification => notification.Message)
                .HasConversion(message => message.Value , value => new Message(value))
                    .HasMaxLength(50)
                    .IsRequired();
            builder.Property(notification => notification.UrlRedirect)
                .HasConversion(urlRedirect => urlRedirect.Value , value => new UrlRedirect(value))
                .HasMaxLength(50)
                 .IsRequired(false);


            builder.Property(n => n.CreatedAt)
                .IsRequired();


            builder.Property(n => n.IsRead)
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}
