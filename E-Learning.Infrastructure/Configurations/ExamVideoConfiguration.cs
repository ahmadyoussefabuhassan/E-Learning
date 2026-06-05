using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


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
            builder.HasOne(e => e.ExamExplanation)
            .WithMany(e => e.ExamExplanationVideos)
            .HasForeignKey(i => i.ExamExplanationId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
