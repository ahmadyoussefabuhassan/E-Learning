using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamExplanations.Commands.UpdateExamExplanation
{
    public sealed record UpdateExamExplanationCommand(
        Guid Id,
        string Title,
        string Description,
        decimal Price
    ) : ICommand<Guid>;

}
