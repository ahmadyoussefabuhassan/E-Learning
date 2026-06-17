using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.ExamVideos.Queries.GetExamVideoById
{
    public sealed record GetExamVideoByIdQuery(Guid videoId) : IQuery<ExamVidoeResponse>;
}
