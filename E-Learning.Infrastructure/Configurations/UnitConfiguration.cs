using E_Learning.Domain.Sections;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable("Units");

            builder.HasKey(u => u.Id); 

            builder.Property(u => u.UnitTitle)
                     .HasConversion(
                          title => title.Value,
                          value => new UnitTitle(value))
                     .IsRequired()
                     .HasMaxLength(30);

            builder.Property(u => u.Description)
            .HasConversion(d => d.Value, d => new Description(d))
            .IsRequired()
            .HasMaxLength(255);

            builder.HasOne(u => u.Section)
                   .WithMany(s => s.Units)
                   .HasForeignKey(u => u.SectionId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
