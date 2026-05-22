using E_Learning.Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            builder.HasMany(student => student.StudentSubscriptions)
                .WithOne(subscription => subscription.Students)
                .HasForeignKey(subscription => subscription.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
