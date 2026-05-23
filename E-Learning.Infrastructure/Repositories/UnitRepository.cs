using E_Learning.Domain.Units;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class UnitRepository : Repository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
