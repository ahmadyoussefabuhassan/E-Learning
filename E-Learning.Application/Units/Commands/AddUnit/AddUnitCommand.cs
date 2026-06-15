using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Units.Commands.AddUnit
{
    public sealed record AddUnitCommand(
        string Title,
        string Description,
        Guid sectionId
    ) : ICommand<Guid>;
}
