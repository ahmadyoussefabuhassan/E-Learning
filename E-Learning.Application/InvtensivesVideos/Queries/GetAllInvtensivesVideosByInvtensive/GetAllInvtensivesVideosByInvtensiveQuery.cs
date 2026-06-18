using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.InvtensivesVideos.Queries.GetAllInvtensivesVideosByInvtensive
{
    public sealed record GetAllInvtensivesVideosByInvtensiveQuery(Guid invtensiveId) : IQuery<IEnumerable<InvtensiveVideoResponse>>;
}
