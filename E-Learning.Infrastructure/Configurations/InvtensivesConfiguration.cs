using E_Learning.Domain.Invtensives;
using E_Learning.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Configurations
{
    internal class InvtensivesConfiguration : IEntityTypeConfiguration<Invtensives>
    {
        public void Configure(EntityTypeBuilder<Invtensives> builder)
        {
            builder.ToTable("Invtensives");

            builder.HasKey(i => i.Id);

            builder.OwnsOne(i => i.Title, titleBuilder =>
            {
                titleBuilder.Property(title => title.Value)
                    .HasColumnName("Title")
                    .HasMaxLength(50)
                    .IsRequired();
            });


            builder.OwnsOne
             (
              d => d.Description , DescriptionBuilder =>
               {
                 DescriptionBuilder.Property(description => description.Value)
                 .HasColumnName("Description")
                 .HasMaxLength(50)
                 .IsRequired();
              }
             );

            builder.OwnsOne
             (
             p => p.Price, PricBuilder =>
              {
                PricBuilder.Property (price => price.Value)
                 .HasColumnType("Price(18,2)")
                 .IsRequired();
              }
            );


            builder .HasOne (Invtensives => Invtensives.Course)
                .WithMany (course => course.Invtensives)
                .HasForeignKey(Invtensives => Invtensives.CourseID) 
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
