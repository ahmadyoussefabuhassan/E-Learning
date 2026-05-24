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

            builder.OwnsOne(i => i.Title, titleBuilder =>
            {
                titleBuilder.Property(title => title.Value)
                    .HasColumnName("Title")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(i => i.Message, messageBuilder =>
            {
                messageBuilder.Property(message => message.Value)
                    .HasColumnName("Message")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder.OwnsOne(i => i.UrlRedirect, urlRedirectBuilder =>
            {
                urlRedirectBuilder.Property(urlRedirect => urlRedirect.Value)
                    .HasColumnName("UrlRedirect")
                    .HasMaxLength(50)
                    .IsRequired();
            });


            builder.Property(n => n.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();


            builder.Property(n => n.IsRead)
                .HasColumnName("IsRead")
                .IsRequired();
        }
    }
}
