using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification.Events;
using E_Learning.Domain.User;
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
        private Notification(Guid id, UserID userId, Message message, Title title, UrlRedirect urlRedirect, CreatedAt createdAt)
            : base(id)
        {
            UserId = userId;
            Message = message;
            Title = title;
            UrlRedirect = urlRedirect;
            CreatedAt = createdAt;
        }
        public UserID UserId { get; private set; }
        public Message Message { get; private set; }
        public Title Title { get; private set; }
        public UrlRedirect UrlRedirect { get; private set; }
        public IsRead  IsRead { get; private set; }
        public CreatedAt CreatedAt { get; private set; }
        public static Notification Create(UserID userId, Message message, Title title, UrlRedirect urlRedirect, CreatedAt createdAt)
        {
            var notification = new Notification(Guid.NewGuid(), userId, message, title, urlRedirect , createdAt);
            notification.RaiseDomainEvent(new NotificationCreatedEvent(notification.Id, notification.UserId.Value , notification.Message.Value, notification.Title.Value, notification.UrlRedirect.Value, notification.IsRead.Value, notification.CreatedAt.Value));
            return notification;
        }
       
    }
}
