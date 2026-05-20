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
        private ExamVideo(Guid id, string videoUrl, int year, Guid examExplanationId) : base(id)
        {
            VideoUrl = videoUrl;
            Year = year;
            ExamExplanationId = examExplanationId;
        }
        public string VideoUrl { get; private set; }
        public int Year { get; private set; }
        public Guid ExamExplanationId { get; private set; }

        public static ExamVideo Create(Guid id, string videoUrl, int year, Guid examExplanationId)
        {
            if (string.IsNullOrWhiteSpace(videoUrl))
                throw new ArgumentException("videoUrl must not be empty", nameof(videoUrl));
            videoUrl = videoUrl.Trim();
            if (videoUrl.Length > 255)
                throw new ArgumentException("videoUrl must be at most 255 characters", nameof(videoUrl));
            if (year < 1900 || year > DateTime.Now.Year)
                throw new ArgumentException("year must be a valid year", nameof(year));
            if (examExplanationId == Guid.Empty)
                throw new ArgumentException("examExplanationId must be a valid Guid", nameof(examExplanationId));
            if (id == Guid.Empty)
                id = Guid.NewGuid();
            var examVideo = new ExamVideo(id, videoUrl, year, examExplanationId);
            examVideo.RaiseDomainEvent(new ExamVideoCreatedDomainEvent(examVideo.Id, examVideo.VideoUrl, examVideo.Year, examVideo.ExamExplanationId));
            return examVideo;
        }
    }
}
