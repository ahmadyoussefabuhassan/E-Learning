using E_Learning.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Role?> GetByNameAsync(Name name, NotType type)
            => await _dbContext.Set<Role>()
                        .FirstOrDefaultAsync(x => x.Name == name && x.notType == type);
    }
}
