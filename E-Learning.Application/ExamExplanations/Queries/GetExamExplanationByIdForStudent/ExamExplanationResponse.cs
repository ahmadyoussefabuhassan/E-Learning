

namespace E_Learning.Application.ExamExplanations.Queries.GetExamExplanationByIdForStudent
{
    public sealed record ExamExplanationResponse(
           Guid Id,
           string Title,
           string Description,
           decimal Price,
           bool IsLocked
    );
}
