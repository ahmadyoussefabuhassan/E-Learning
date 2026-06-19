using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.StudentSubscription.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.StudentSubscriptions.Events
{
    public sealed class SubscriptionConfirmedEventHandler : INotificationHandler<SubscriptionConfirmedDomainEvent>
    {
        private readonly INotificationService _notificationService;

        public SubscriptionConfirmedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(SubscriptionConfirmedDomainEvent notification, CancellationToken cancellationToken)
        {
            var template = SubscriptionNotifications.Accepted;

            // إرسال للطالب المحدد وحفظ الإشعار في سجلاته
            await _notificationService.SendToUserAsync(
                notification.StudentId,
                template.Title,
                template.Description,
                cancellationToken);
        }
    }
}
