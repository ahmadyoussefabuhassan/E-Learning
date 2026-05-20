using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Notification
{
    public class Notification : Entity
    { 
        private Notification() : base(Guid.Empty)
        {
        }
        private Notification(Guid id, Guid userId, string message, string title,string urlRedirect , DateTime createdAt)
            : base(id)
        {
            UserId = userId;
            Message = message;
            Title = title;
            UrlRedirect = urlRedirect;
            CreatedAt = createdAt;
        }
        public Guid UserId { get; private set; }
        public string Message { get; private set; }
        public string Title { get; private set; }
        public string UrlRedirect { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public static Notification Create(Guid userId, string message, string title, string urlRedirect, DateTime createdAt)
        {
            var notification = new Notification(Guid.NewGuid(), userId, message, title, urlRedirect , createdAt);
            notification.RaiseDomainEvent(new NotificationCreatedEvent(notification.Id, notification.UserId, notification.Message, notification.Title, notification.UrlRedirect, notification.IsRead, notification.CreatedAt));
            return notification;
        }
        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                RaiseDomainEvent(new NotificationReadEvent(Id));
            }
        }
    }
}
