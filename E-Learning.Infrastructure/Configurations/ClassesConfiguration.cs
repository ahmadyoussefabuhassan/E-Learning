using E_Learning.Domain.Classes;
using E_Learning.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class ClassesConfiguration : IEntityTypeConfiguration<Classes>
    {
        public void Configure(EntityTypeBuilder<Classes> builder)
        {
          builder.ToTable ("Classes");

          builder .HasKey (c => c.Id);
             
            builder.OwnsOne(r => r.Name, nameBuilder =>
            {
                nameBuilder.Property(name => name.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}
