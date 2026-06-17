using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.ExamVideos.Commands.AddExamVideo
{
    public sealed record AddExamVideoCommand(Guid ExamId , IFormFile VidoUrl , int Year ) : ICommand<Guid>; 
}
