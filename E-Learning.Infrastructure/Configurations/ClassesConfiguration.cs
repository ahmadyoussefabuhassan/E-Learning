using E_Learning.Domain.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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
