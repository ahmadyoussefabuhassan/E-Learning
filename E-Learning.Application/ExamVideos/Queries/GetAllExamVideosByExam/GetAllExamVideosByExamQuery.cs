using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.ExamVideos.Queries.GetAllExamVideosByExam
{
    public sealed record GetAllExamVideosByExamQuery(Guid ExamId) : IQuery<IEnumerable<ExamVidoeResponse>>;
}
