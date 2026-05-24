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
            builder.HasKey(i => i.Id);
            builder.ToTable("StudentSubscriptions");

            builder.OwnsOne(tt => tt.TargetType, TargetTypeBuilder =>
            {
                TargetTypeBuilder.Property(t => t.Value)
            .HasColumnName("TargetType")
                    .HasMaxLength(50)
                    .IsRequired();
            });

                builder.OwnsOne(r => r.ReceiptImageUrl, ReceiptImageUrlBuilder =>
                {
                    ReceiptImageUrlBuilder.Property(r => r.Value)
                .HasColumnName("ReceiptImageUrl")
                        .HasMaxLength(200)
                        .IsRequired();
                });
    
                builder.OwnsOne(p => p.PriceAtPurchase, PriceAtPurchaseBuilder =>
                {
                    PriceAtPurchaseBuilder.Property(p => p.Value)
                .HasColumnType("PriceAtPurchase(18,2)")
                        .HasPrecision(18, 2)
                        .IsRequired();
                });           
            builder .HasOne(StudentSubscriptions => StudentSubscriptions.Students)
                .WithMany(Students => Students.StudentSubscriptions)
                .HasForeignKey(StudentSubscriptions => StudentSubscriptions.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

            
        
    }
}