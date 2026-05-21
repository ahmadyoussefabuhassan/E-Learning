using E_Learning.Domain.Abstractions;
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

        private InvtensivesVideos(Guid id, Guid invtensiveId, string videoUrl) : base(id)
        {
            InvtensiveId = invtensiveId;
            VideoUrl = videoUrl;
        }

        public Guid InvtensiveId { get; private set; }
        public string VideoUrl { get; private set; }

        public static InvtensivesVideos Create(Guid invtensiveId, string videoUrl)
        {
            var invtensivesVideo = new InvtensivesVideos(Guid.NewGuid(), invtensiveId, videoUrl);
            invtensivesVideo.RaiseDomainEvent(new InvtensivesVideosCreatedEvent(invtensivesVideo.Id, invtensivesVideo.InvtensiveId, invtensivesVideo.VideoUrl));
            return invtensivesVideo;
        }
    }
}