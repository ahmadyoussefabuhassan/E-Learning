using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.InvtensivesVideos
{
    public interface IInvtensivesVideosRepositry : IRepository<InvtensivesVideos>
    {
        Task<IEnumerable<InvtensivesVideos>> GetAllByInvtensiveAsync(Guid invtensiveId , CancellationToken cancellation);
    }
   
}
