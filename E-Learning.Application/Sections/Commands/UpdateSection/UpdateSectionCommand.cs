using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Sections.Commands.UpdateSection
{
    public sealed record UpdateSectionCommand(
        Guid Id,
        string Title,
        decimal Price 
    ) : ICommand<Guid>;
}
