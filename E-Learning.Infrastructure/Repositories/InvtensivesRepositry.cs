using E_Learning.Domain.Invtensives;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class InvtensivesRepositry : Repository<Invtensives>, IInvtensivesRepositry
    {
        public InvtensivesRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
