using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamVideos.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamVideos
{
    public class ExamVideo : Entity
    {
        private ExamVideo(Guid id) : base(id)
        {
        }
        private ExamVideo(Guid id, VideoUrl videoUrl, Year year, Guid examExplanationId) : base(id)
        {
            VideoUrl = videoUrl;
            Year = year;
            ExamExplanationId = examExplanationId;
        }
        public VideoUrl VideoUrl { get; private set; }
        public Year Year { get; private set; }
        public Guid ExamExplanationId { get; private set; }

        public static ExamVideo Create(Guid id, VideoUrl videoUrl, Year year, Guid examExplanationId)
        {
            
            var examVideo = new ExamVideo(id, videoUrl, year, examExplanationId);
            examVideo.RaiseDomainEvent(new ExamVideoCreatedDomainEvent(examVideo.Id, examVideo.VideoUrl.Value, examVideo.Year.Value, examVideo.ExamExplanationId));
            return examVideo;
        }
    }
}
