using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Invtensives.Commands.DeleteInvtensive
{
    public sealed record DeleteInvtensiveCommand(Guid Id) : ICommand;
}
