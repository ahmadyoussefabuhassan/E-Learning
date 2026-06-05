using E_Learning.Domain.Students;
using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Table name
            builder.ToTable("Students");
            // Primary key
            builder.HasKey(s => s.Id);
            // Properties
            builder.Property(student => student.SubjectStudent)
                .HasConversion(sub => sub.Value, sub => new SubjectStudent(sub))
                .HasMaxLength(50)
                .IsRequired();
            // Relationships
            builder.HasOne(s => s.User)
                .WithOne(user => user.Student)
                .HasForeignKey<Student>(student => student.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
