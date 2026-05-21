using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.InvtensivesVideos.Events
{
    public sealed record InvtensivesVideosCreatedEvent(Guid id, Guid invtensiveId, string videoUrl) : IDomainEvent;
    
}