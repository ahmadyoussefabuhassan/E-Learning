using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
                 => await _dbContext.Set<User>()
            .FirstOrDefaultAsync(x => x.Email.Value == email.Value, cancellationToken);

        public async Task<User?> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken)
            => await _dbContext.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Value == email.Value, cancellationToken);
    }
}
