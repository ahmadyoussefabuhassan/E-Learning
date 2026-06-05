using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
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
            builder.HasOne(t => t.User)
                .WithOne(user => user.Teacher)
                .HasForeignKey<Teacher>(teacher => teacher.Id)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
