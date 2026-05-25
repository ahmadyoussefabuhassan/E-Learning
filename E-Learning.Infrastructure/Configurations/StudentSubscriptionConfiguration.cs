using E_Learning.Domain.StudentSubscription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class StudentSubscriptionConfiguration : IEntityTypeConfiguration<StudentSubscription>
    {
        public StudentSubscriptionConfiguration() { }

        public void Configure(EntityTypeBuilder<StudentSubscription> builder)
        {
           // Table name
            builder.ToTable("StudentSubscriptions");
            // Primary key
            builder.HasKey(studentsubscriptions => studentsubscriptions.Id);
            // Properties
            builder.Property(studentsubscriptions => studentsubscriptions.TargetType )
                .HasConversion(targettype => targettype.Value , value  => new TargetType(value))
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(studentsubscriptions => studentsubscriptions.ReceiptImageUrl)
                .HasConversion(receiptimageurl => receiptimageurl.Value , value => new ReceiptImageUrl(value))
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(studentsubscriptions => studentsubscriptions.PriceAtPurchase)
                .HasConversion(priceAtpurchase => priceAtpurchase.Value, value => new PriceAtPurchase(value))
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }

            
        
    }
}