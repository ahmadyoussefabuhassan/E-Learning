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
            //
            builder.ToTable("ExamVideos");
            //
            builder.HasKey(examvideo => examvideo.Id);
            //
            builder.Property(examvideo => examvideo.VideoUrl)
                .HasConversion(examvideosvideourl => examvideosvideourl.Value, v => new ExamVideosVideoUrl(v))
                .IsRequired();

            builder.Property(examvideo => examvideo.Year)
                .HasConversion(year => year.Value, y => new Year(y))
                .IsRequired();
            //
            builder.HasOne<ExamExplanation>()
            .WithMany()
            .HasForeignKey(i => i.ExamExplanationId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
