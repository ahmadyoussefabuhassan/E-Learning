using E_Learning.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class NotificationRepositry : Repository<Notification>, INotificationRepositry
    {
        public NotificationRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
