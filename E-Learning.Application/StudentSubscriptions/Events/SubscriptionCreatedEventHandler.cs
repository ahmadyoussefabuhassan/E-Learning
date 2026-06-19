using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.StudentSubscription.Events;
using MediatR;

namespace E_Learning.Application.StudentSubscriptions.Events
{
    public sealed class SubscriptionCreatedEventHandler : INotificationHandler<StudentSubscriptionCreatedEvent>
    {
        private readonly INotificationService _notificationService;

        public SubscriptionCreatedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(StudentSubscriptionCreatedEvent notification, CancellationToken cancellationToken)
        {
            var template = SubscriptionNotifications.RequestReceived;
            await _notificationService.SendToGroupAsync("Admins", template.Title, template.Description, cancellationToken);
        }
    }
}
