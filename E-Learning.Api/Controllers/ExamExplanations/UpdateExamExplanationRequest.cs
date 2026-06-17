namespace E_Learning.Api.Controllers.ExamExplanations
{
    public sealed record UpdateExamExplanationRequest(
        string Title,
        string Description,
        decimal Price
    );
}
