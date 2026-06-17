using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamExplanations.Commands.AddExamExplanation
{
    public sealed record AddExamExplanationCommand(
        Guid CourseId,
        string Title ,
        string Description ,
        decimal Price
    ) : ICommand<Guid> ; 
}
