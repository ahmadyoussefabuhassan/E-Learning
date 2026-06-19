using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterSection
{
    public sealed record RegisterSectionCommand(
        Guid targetId,
        IFormFile ReceiptImageUrl
    ) : ICommand<Guid>;

}
