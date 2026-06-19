using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterCourse
{
    public sealed record RegisterCourseCommand(
        Guid targetId,
        IFormFile ReceiptImageUrl
    ) : ICommand<Guid>;
}
