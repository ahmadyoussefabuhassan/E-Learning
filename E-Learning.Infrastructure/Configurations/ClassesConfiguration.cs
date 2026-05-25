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

             builder.HasKey(classes => classes.Id);

            builder.Property(classes => classes.Name)
                 .HasConversion(name  => name.Value , value => new ClassesName(value))
                 .HasMaxLength(50)
                 .IsRequired();

        }
    }
}
