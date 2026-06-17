using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.ExamVideos.Commands.UpdateExamVideo
{
    public sealed record UpdateExamVideoCommand(Guid Id , IFormFile VidoUrl , int Year) : ICommand<Guid>;
}
