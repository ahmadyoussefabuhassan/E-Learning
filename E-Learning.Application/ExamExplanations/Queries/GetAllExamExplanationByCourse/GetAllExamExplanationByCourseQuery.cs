using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamExplanations.Queries.GetAllExamExplanationByCourse
{
    public sealed record GetAllExamExplanationByCourseQuery(Guid courseId) : IQuery<IEnumerable<ExamExplanationResponse>>;
}
