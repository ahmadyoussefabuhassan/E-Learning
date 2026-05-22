using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification.Events;

namespace E_Learning.Domain.Notification
{
    public sealed class Notification : Entity
    { 
        private Notification() : base(Guid.Empty)
        {
        }
        private Notification(Guid id, Guid userId, Message message, Title title, UrlRedirect urlRedirect, DateTime createdAt)
            : base(id)
        {
            UserId = userId;
            Message = message;
            Title = title;
            UrlRedirect = urlRedirect;
            CreatedAt = createdAt;
        }
        public Guid UserId { get; private set; }
        public Message Message { get; private set; }
        public Title Title { get; private set; }
        public UrlRedirect UrlRedirect { get; private set; }
        public bool  IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public static Notification Create(Guid userId, Message message, Title title, UrlRedirect urlRedirect)
        {
            var notification = new Notification(Guid.NewGuid(), userId, message, title, urlRedirect, DateTime.UtcNow);
            notification.RaiseDomainEvent(new NotificationCreatedEvent(notification.Id, notification.UserId, notification.Message.Value, notification.Title.Value, notification.UrlRedirect.Value, notification.IsRead, notification.CreatedAt));
            return notification;
        }
       
    }
}
