using E_Learning.Domain.Notification;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class NotificationRepositry : Repository<Notification>, INotificationRepositry
    {
        public NotificationRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await _dbContext.Set<Notification>()
                    .AsNoTracking()
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedAt) 
                    .ToListAsync(cancellationToken);
    }
}
