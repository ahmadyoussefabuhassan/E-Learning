using E_Learning.Domain.Notification;


namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class NotificationRepositry : Repository<Notification>, INotificationRepositry
    {
        public NotificationRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
