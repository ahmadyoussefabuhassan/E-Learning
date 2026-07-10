using E_Learning.Domain.Notification;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class NotificationRepositry : Repository<Notification>, INotificationRepositry
    {
        public NotificationRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllNotificationByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var notifications = await _dbContext.Set<Notification>()
                .Where(n => n.UserId == userId)
                .ToListAsync(cancellationToken);
            _dbContext.Set<Notification>().RemoveRange(notifications);
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
            => await _dbContext.Set<Notification>()
                    .AsNoTracking()
                    .Where(n => n.UserId == userId)
                    .OrderByDescending(n => n.CreatedAt) 
                    .ToListAsync(cancellationToken);
    }
}
