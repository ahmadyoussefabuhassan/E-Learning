namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllExamExplanationsSubscriptionsByStudent
{
    public sealed record ExamExplanationResponse(
           Guid Id,
           string Title,
           string Description,
           decimal Price,
           bool IsLocked
    );
}
