using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Units.Commands.UpdateUnit
{
    public sealed record UpdateUnitCommand(
        Guid Id,
        string Title,
        string Description
    ) : ICommand<Guid>;
}
