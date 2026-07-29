
using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamExplanations.Queries.GetExamExplanationByIdForStudent
{
    public sealed record GetExamExplanationByIdForStudentQuery(Guid Id) : IQuery<ExamExplanationResponse>;
}
