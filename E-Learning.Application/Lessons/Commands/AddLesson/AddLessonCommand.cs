using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Commands.AddLesson
{
    public sealed record AddLessonCommand(
        Guid unitId , 
        string Title , 
        string TitleUrl ,
        IFormFile VidoUrl
    ) : ICommand<Guid>; 
}
