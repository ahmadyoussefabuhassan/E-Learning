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
            //
            builder.ToTable("ExamExplanations");
            //
            builder.HasKey(examexplanation => examexplanation.Id);
            //
            builder.Property(examexplanation => examexplanation.Title)
                .HasConversion(title => title.Value, t => new Title(t))
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(examexplanation => examexplanation.Description)
                .HasConversion(description => description.Value, d => new Description(d))
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(examexplanation => examexplanation.Price)
                .HasConversion(price => price.Value, p => new Price(p))
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            //
            builder.HasOne<Course>()
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
