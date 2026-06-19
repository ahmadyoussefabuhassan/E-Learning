using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterInvtensive
{
    public sealed record RegisterInvtensiveCommand(
        Guid targetId,
        IFormFile ReceiptImageUrl
    ) : ICommand<Guid>;
}
