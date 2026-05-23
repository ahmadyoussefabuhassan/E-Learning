using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class ExamExplanationConfiguration : IEntityTypeConfiguration<ExamExplanation>
    {
        public void Configure(EntityTypeBuilder<ExamExplanation> builder)
        {
            builder.ToTable("ExamExplanations");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasConversion(t => t.Value, t => new Title(t))
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasConversion(d => d.Value, d => new Description(d))
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Price)
                .HasConversion(p => p.Value, p => new Price(p))
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            builder.HasOne<Course>()
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
