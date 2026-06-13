using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Sections.Commands.AddSection
{
    public sealed record AddSectionCommand(
        string Title,
        decimal Price ,
        Guid CourseId
    ) : ICommand<Guid>;
}
