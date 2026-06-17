using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamExplanations.Commands.DeleteExamExplanation
{
    public sealed record DeleteExamExplanationCommand(Guid ExamId) : ICommand<bool>;
}
