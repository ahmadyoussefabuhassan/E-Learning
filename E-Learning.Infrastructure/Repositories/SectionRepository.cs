using E_Learning.Domain.Sections;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
