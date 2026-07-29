using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Commands.UpdateInvtensiveVideo
{
    public sealed record UpdateInvtensiveVideoCommand(Guid Id ,string TitleUrl,IFormFile VideoUrl): ICommand<Guid>;
}
