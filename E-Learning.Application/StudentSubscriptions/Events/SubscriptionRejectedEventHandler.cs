using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.StudentSubscription.Events;
using MediatR;

namespace E_Learning.Application.StudentSubscriptions.Events
{
    public sealed class SubscriptionRejectedEventHandler : INotificationHandler<SubscriptionRejectedDomainEvent>
    {
        private readonly INotificationService _notificationService;

        public SubscriptionRejectedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(SubscriptionRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            var template = SubscriptionNotifications.Rejected;

            await _notificationService.SendToUserAsync(
                notification.StudentId,
                template.Title,
                template.Description,
                cancellationToken);
        }
    }
}
