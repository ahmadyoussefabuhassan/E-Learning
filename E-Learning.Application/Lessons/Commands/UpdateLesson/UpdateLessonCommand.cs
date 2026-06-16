using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Commands.UpdateLesson
{
    public sealed record UpdateLessonCommand(
        Guid Id,
        string Title,
        string TitleUrl,
        IFormFile VidoUrl
    ):ICommand<Guid>;
}
