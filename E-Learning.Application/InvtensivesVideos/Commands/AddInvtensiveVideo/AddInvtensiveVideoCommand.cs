using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Commands.AddInvtensiveVideo
{
    public sealed record AddInvtensiveVideoCommand(Guid invtensiveId  ,string TitleUrl, IFormFile VidoeUrl) : ICommand<Guid>;
}
