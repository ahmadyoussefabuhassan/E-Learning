using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Sections.Commands.DeleteSection
{
    public sealed record DeleteSectionCommand(Guid Id) : ICommand<bool>;
}
