using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllExamExplanationsSubscriptionsByStudent
{
    public sealed record GetAllExamExplanationsSubscriptionsByStudentQuery() : IQuery<IEnumerable<ExamExplanationResponse>>;
}
