using E_Learning.Domain.Invtensives;
using E_Learning.Domain.InvtensivesVideos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Learning.Infrastructure.Configurations
{
    internal sealed class NotificationConfigurationConfiguration : IEntityTypeConfiguration<InvtensivesVideos>
    {
        public void Configure(EntityTypeBuilder<InvtensivesVideos> builder)
        {

           builder.ToTable("InvtensivesVideos");
           
            builder .HasKey (i=>i.Id);

            builder.Property(IntV => IntV.VideoUrl)
                .HasConversion(videourl => videourl.Value , value => new InvtensivesVideosVideoUrl(value))
                .HasMaxLength(500)
                .IsRequired(); 
            builder.Property(IntV => IntV.TitleVideoUrl)
                .HasConversion(titleVideoUrl => titleVideoUrl.Value , value => new TitleVideoUrl(value))
                .HasMaxLength(500)
                .IsRequired();
            builder.HasOne(inv => inv.Invtensive)
                .WithMany(Invtensive => Invtensive.InvtensivesVideos)
                .HasForeignKey(InvtensivesVideos => InvtensivesVideos.InvtensiveId)
                .OnDelete(DeleteBehavior.Cascade);

        }

       
    }
}
