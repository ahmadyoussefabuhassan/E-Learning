using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(l => l.Id);

            builder.Property(lessons => lessons.LessonTitle)
                .HasConversion(title => title.Value, t => new LessonTitle(t))
                .HasMaxLength(255)
                .IsRequired();


            builder.Property(lessons => lessons.URL)
                .HasConversion(u => u.Value, u => new URL(u))
                .HasMaxLength(255);


            builder.Property(lessons => lessons.TitleUrl)
               .HasConversion(t => t.Value, t => new TitleUrl(t))
               .HasMaxLength(50);

            builder.HasOne<Unit>()
                .WithMany()
                .HasForeignKey(lessons => lessons.UnitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
