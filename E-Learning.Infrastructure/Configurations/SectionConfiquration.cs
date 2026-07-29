using E_Learning.Domain.Courses;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class SectionConfiquration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SectionTitle)
                .HasConversion(t => t.Value, t => new SectionTitle(t))
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(s => s.Price)
                 .HasConversion(p => p.Value, p => new Price(p))
                 .IsRequired()
                 .HasColumnType("decimal(18,2)");

            builder.Property(s => s.IsLocked)
                   .HasDefaultValue(true)
                   .IsRequired();


            builder.HasOne(s => s.Course) 
                   .WithMany(c => c.Sections)
                   .HasForeignKey(s => s.CourseId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
