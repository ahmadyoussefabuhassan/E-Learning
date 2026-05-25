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
            builder.Property(Inv => Inv.Title)
                 .HasConversion(title => title.Value , value => new InvtensivesTitle(value))
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(Inv => Inv.Description)
                .HasConversion(description => description.Value , value => new Description(value))
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(Inv => Inv.Price)
                .HasConversion(price => price.Value  , value => new Price(value))
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            //
            builder .HasOne (Invtensives => Invtensives.Course)
                .WithMany (course => course.Invtensives)
                .HasForeignKey(Invtensives => Invtensives.CourseID) 
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
