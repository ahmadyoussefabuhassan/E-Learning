using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.StudentSubscriptions.Commands.RegiterExamExplanation
{
    public sealed record RegiterExamExplanationCommand(
        Guid targetId,
        IFormFile ReceiptImageUrl
    ) : ICommand<Guid>;
}
