namespace E_Learning.Api.Controllers.ExamExplanations
{
    public sealed record AddExamExplanationRequest(
        string Title,
        string Description,
        decimal Price

    );
}
