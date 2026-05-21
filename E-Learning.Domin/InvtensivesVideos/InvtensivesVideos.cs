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

        private InvtensivesVideos(Guid id, InvtensiveId invtensiveId, VideoUrl videoUrl) : base(id)
        {
            InvtensiveId = invtensiveId;
            VideoUrl = videoUrl;   
        }

        public InvtensiveId InvtensiveId { get; private set; }
        public VideoUrl VideoUrl { get; private set; }

        public static InvtensivesVideos Create(InvtensiveId invtensiveId, VideoUrl videoUrl)
        {
            var invtensivesVideo = new InvtensivesVideos(Guid.NewGuid(), invtensiveId, videoUrl);
            invtensivesVideo.RaiseDomainEvent(new InvtensivesVideosCreatedEvent(invtensivesVideo.Id, invtensivesVideo.InvtensiveId.Value, invtensivesVideo.VideoUrl.Value));
            return invtensivesVideo;
        }
    }
}