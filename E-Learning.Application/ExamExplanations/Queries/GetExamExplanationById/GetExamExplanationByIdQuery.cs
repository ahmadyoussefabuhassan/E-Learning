using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamExplanations.Queries.GetExamExplanationById
{
    public sealed record GetExamExplanationByIdQuery(Guid examId) : IQuery<ExamExplanationResponse>;
}
