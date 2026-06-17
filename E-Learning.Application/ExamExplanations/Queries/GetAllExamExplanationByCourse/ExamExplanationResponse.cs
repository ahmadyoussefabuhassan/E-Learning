

namespace E_Learning.Application.ExamExplanations.Queries.GetAllExamExplanationByCourse
{
    public sealed record ExamExplanationResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price
    );
}
