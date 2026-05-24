using E_Learning.Domain.InvtensivesVideos;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class InvtensivesVideosRepositry : Repository<InvtensivesVideos>, IInvtensivesVideosRepositry
    {
        public InvtensivesVideosRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
