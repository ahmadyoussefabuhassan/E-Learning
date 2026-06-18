using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoById
{
    public sealed record GetInvtensiveVideoByIdQuery(Guid Id) : IQuery<InvtensiveVideoResponse>;
}
