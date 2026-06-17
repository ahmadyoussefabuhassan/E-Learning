

namespace E_Learning.Application.ExamExplanations.Queries.GetExamExplanationById
{
    public sealed record ExamExplanationResponse(
         Guid Id,
         string Title,
         string Description,
         decimal Price
     );
}
