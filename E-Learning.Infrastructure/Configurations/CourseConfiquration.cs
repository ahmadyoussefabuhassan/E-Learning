using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Teachers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class CourseConfiquration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CourseName)
                .HasConversion(n => n.Value, n => new CourseName(n))
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.ImageUrl)
                .HasConversion(i => i.Value, i => new ImageUrl(i))
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Description)
                .HasConversion(d => d.Value, d => new Description(d))
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.IsActive)
            .IsRequired();
            builder.Property(p => p.Price)
                .HasConversion(p => p.Value, p => new Price(p))
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne<Classes>()
                .WithMany()
                .HasForeignKey(c => c.ClassesId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Teacher>()
                .WithMany()
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
