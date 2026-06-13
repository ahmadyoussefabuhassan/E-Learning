using E_Learning.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<User>().Include(u => u.Role)
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        public async Task<User?> GetByEmailAsync(Domain.User.Email email, CancellationToken cancellationToken)
                 => await _dbContext.Set<User>().Include(u => u.Role)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        public async Task<User?> IsEmailUniqueAsync(Domain.User.Email email, CancellationToken cancellationToken)
            => await _dbContext.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);

        public async Task<User?> GetResetCodeAsync(string resetCode, CancellationToken cancellationToken)
            => await _dbContext.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.PasswordResetCode.Value == resetCode, cancellationToken);

        public async Task<int> GetCountUserssAsync(CancellationToken cancellation)
            => await _dbContext.Set<User>()
            .CountAsync(cancellation);
    }
}
