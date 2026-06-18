using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoStream
{
    public sealed record GetInvtensiveVideoStreamQuery(Guid Id) : IQuery<FileStream>;
}
