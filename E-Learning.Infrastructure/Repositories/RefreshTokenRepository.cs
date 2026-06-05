using E_Learning.Domain.RefreshTokens;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public RefreshTokenRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;
        public async Task AddSaveToken(RefreshToken token)
            => await _dbContext.Set<RefreshToken>().AddAsync(token);

        public async Task DeleteToken(string token)
        {
            var t = await _dbContext.Set<RefreshToken>()
                 .FirstOrDefaultAsync(x => x.Token == token);
            if (t is not null)
                _dbContext.Set<RefreshToken>().Remove(t);
        }

        public async Task<RefreshToken?> GetToken(string token)
            => await _dbContext.Set<RefreshToken>()
                        .FirstOrDefaultAsync(x => x.Token == token );
    }
}
