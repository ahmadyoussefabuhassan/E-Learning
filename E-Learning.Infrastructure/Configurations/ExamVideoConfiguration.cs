using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class ExamVideoConfiguration : IEntityTypeConfiguration<ExamVideo>
    {
        public void Configure(EntityTypeBuilder<ExamVideo> builder)
        {
            builder.ToTable("ExamVideos");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.VideoUrl)
                .HasConversion(v => v.Value, v => new ExamVideosVideoUrl(v))
                .IsRequired();

            builder.Property(e => e.Year)
                .HasConversion(y => y.Value, y => new Year(y))
                .IsRequired();

            builder.HasOne<ExamExplanation>()
            .WithMany()
            .HasForeignKey(i => i.ExamExplanationId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
