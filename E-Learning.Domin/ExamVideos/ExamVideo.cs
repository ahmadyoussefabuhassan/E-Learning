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
        private ExamVideo(Guid id, ExamVideosVideoUrl videoUrl, Year year, TitleVideoUrl titleVideoUrl,Guid examExplanationId) : base(id)
        {
            VideoUrl = videoUrl;
            Year = year;
            TitleVideoUrl = titleVideoUrl;
            ExamExplanationId = examExplanationId;
        }
        public ExamVideosVideoUrl VideoUrl { get; private set; }
        public Year Year { get; private set; }
        public TitleVideoUrl TitleVideoUrl { get; private set; } = null!;
        public Guid ExamExplanationId { get; private set; }
        public ExamExplanation ExamExplanation { get; private set; } = null!;

        public static ExamVideo Create( ExamVideosVideoUrl videoUrl, Year year, TitleVideoUrl titleVideoUrl,Guid examExplanationId )
        {
            
            var examVideo = new ExamVideo(Guid.NewGuid(), videoUrl, year, titleVideoUrl,examExplanationId);
            examVideo.RaiseDomainEvent(new ExamVideoCreatedDomainEvent(examVideo.Id, examVideo.VideoUrl.Value, examVideo.Year.Value, examVideo.ExamExplanationId));
            return examVideo;
        }
        public void UpdateVidoe(ExamVideosVideoUrl videoUrl , Year year , TitleVideoUrl titleVideoUrl)
        {
            VideoUrl = videoUrl;
            Year = year;
            TitleVideoUrl = titleVideoUrl;
        }
    }
}
