using E_Learning.Domain.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            // Table Name
            builder.ToTable("Teachers");
            // Primary Key
            builder.HasKey(x => x.Id);
            // Properties
            builder.Property(teacher => teacher.SubjectTeacher)
                .HasMaxLength(50)
                .HasConversion(sub => sub.Value, value => new SubjectTeacher(value))
                .IsRequired();
            builder.Property(teacher => teacher.UrlShamCash)
                .HasMaxLength(200)
                .HasConversion(url => url != null ? url.Value : null, value => value != null ?  new UrlShamCash(value) : null)
                .IsRequired(false);
            // Relationships
              builder.HasMany(teacher => teacher.Courses)
                   .WithOne(course => course.Teachers)
                    .HasForeignKey(course => course.TeacherId)
                    .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
