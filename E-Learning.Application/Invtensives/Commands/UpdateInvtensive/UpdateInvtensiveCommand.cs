

using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Invtensives.Commands.UpdateInvtensive
{
    public sealed record UpdateInvtensiveCommand(
        Guid Id,
        string Title,
        string Description,
        decimal Price
    ) : ICommand<Guid>;
}
