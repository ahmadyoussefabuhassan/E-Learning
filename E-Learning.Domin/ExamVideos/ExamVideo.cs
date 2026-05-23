using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos.Event;


namespace E_Learning.Domain.ExamVideos
{
    public sealed class ExamVideo : Entity
    {
        private ExamVideo() : base(Guid.Empty)
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
        public ExamExplanation? ExamExplanation { get; private set; }

        public static ExamVideo Create(VideoUrl videoUrl, Year year, Guid examExplanationId)
        {
            
            var examVideo = new ExamVideo(Guid.NewGuid(), videoUrl, year, examExplanationId);
            examVideo.RaiseDomainEvent(new ExamVideoCreatedDomainEvent(examVideo.Id, examVideo.VideoUrl.Value, examVideo.Year.Value, examVideo.ExamExplanationId));
            return examVideo;
        }
    }
}
