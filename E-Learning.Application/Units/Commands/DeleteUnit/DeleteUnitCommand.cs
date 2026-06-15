using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Units.Commands.DeleteUnit
{
    public sealed record DeleteUnitCommand(Guid Id) : ICommand<bool>;
}
