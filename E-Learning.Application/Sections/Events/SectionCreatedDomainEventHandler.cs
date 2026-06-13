

using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Sections.Events;
using E_Learning.Domain.StudentSubscription;
using MediatR;

namespace E_Learning.Application.Sections.Events
{
    public sealed class SectionCreatedDomainEventHandler : INotificationHandler<SectionCreatedDomainEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IStudentSubscriptionRepositry _subscriptionRepo;

        public SectionCreatedDomainEventHandler(INotificationService notificationService, IStudentSubscriptionRepositry subscriptionRepo)
        {
            _notificationService = notificationService;
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task Handle(SectionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var allStudentIds = await _subscriptionRepo.GetAllStudentIdsAsync(cancellationToken);

            if (allStudentIds.Any())
            {
                var template = SectionNotifications.Created;
                var message = string.Format(template.Description, notification.Title);

                await _notificationService.SendToUsersAsync(allStudentIds, template.Title, message, cancellationToken);
            }
        }
    }
}
