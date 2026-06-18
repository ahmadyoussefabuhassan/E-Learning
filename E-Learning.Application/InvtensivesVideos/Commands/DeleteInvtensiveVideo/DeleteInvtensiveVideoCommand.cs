using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.InvtensivesVideos.Commands.DeleteInvtensiveVideo
{
    public sealed record DeleteInvtensiveVideoCommand(Guid Id) : ICommand<bool>;
}
