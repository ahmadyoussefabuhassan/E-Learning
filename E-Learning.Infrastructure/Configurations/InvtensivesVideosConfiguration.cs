using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.InvtensivesVideos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class NotificationConfigurationConfiguration : IEntityTypeConfiguration<InvtensivesVideos>
    {
        public void Configure(EntityTypeBuilder<InvtensivesVideos> builder)
        {
           builder.ToTable("InvtensivesVideos");
           builder .HasKey (i=>i.Id);

            builder.OwnsOne(i => i.VideoUrl, VideoUrlBuilder =>
            {
                VideoUrlBuilder.Property(VideoUrl => VideoUrl.Value)
                    .HasColumnName("VideoUrl")
                    .HasMaxLength(50)
                    .IsRequired();
            });

            builder .HasOne(InvtensivesVideos => InvtensivesVideos.Invtensive)
                .WithMany(Invtensive => Invtensive.InvtensivesVideos)
                .HasForeignKey(InvtensivesVideos => InvtensivesVideos.InvtensiveId)
                .OnDelete(DeleteBehavior.Cascade);

        }

       
    }
}
