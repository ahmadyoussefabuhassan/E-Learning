using E_Learning.Domain.Abstractions;
using E_Learning.Domain.InvtensivesVideos.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.InvtensivesVideos
{
    public class InvtensivesVideos : Entity 
    {
        private InvtensivesVideos(): base(Guid.Empty) { }

        private InvtensivesVideos(Guid id, Guid invtensiveId, InvtensivesVideosVideoUrl videoUrl , TitleVideoUrl titleVideoUrl) : base(id)
        {
            InvtensiveId = invtensiveId;
            VideoUrl = videoUrl;
            TitleVideoUrl = titleVideoUrl;
        }

        public Guid InvtensiveId { get; private set; }
        public Invtensives.Invtensives Invtensive { get; private set; } = null!;
        public InvtensivesVideosVideoUrl VideoUrl { get; private set; }
        public TitleVideoUrl TitleVideoUrl { get; private set; } = null!;

        public static InvtensivesVideos Create(Guid invtensiveId, InvtensivesVideosVideoUrl videoUrl , TitleVideoUrl titleVideoUrl)
        {
            var invtensivesVideo = new InvtensivesVideos(Guid.NewGuid(), invtensiveId, videoUrl , titleVideoUrl);
            invtensivesVideo.RaiseDomainEvent(new InvtensivesVideosCreatedEvent(invtensivesVideo.Id, invtensivesVideo.InvtensiveId, invtensivesVideo.VideoUrl.Value));
            return invtensivesVideo;
        }
        public void UpdateVideo(InvtensivesVideosVideoUrl videoUrl , TitleVideoUrl titleVideoUrl)
            =>(VideoUrl, TitleVideoUrl) = (videoUrl, titleVideoUrl);
    }
}