using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamVideos.Queries.GetExamVideoStream
{
    public sealed record GetExamVideoStreamQuery(Guid examvideoId) : IQuery<FileStream>;
}
