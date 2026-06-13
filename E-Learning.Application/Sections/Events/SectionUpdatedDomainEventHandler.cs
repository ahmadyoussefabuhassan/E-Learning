using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Sections.Events;
using E_Learning.Domain.StudentSubscription;
using MediatR;

namespace E_Learning.Application.Sections.Events
{
    public sealed class SectionUpdatedDomainEventHandler : INotificationHandler<SectionUpdatedDomainEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IStudentSubscriptionRepositry _subscriptionRepo;

        public SectionUpdatedDomainEventHandler(INotificationService notificationService, IStudentSubscriptionRepositry subscriptionRepo)
        {
            _notificationService = notificationService;
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task Handle(SectionUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var studentIds = await _subscriptionRepo.GetSubscribedStudentIdsAsync(notification.CourseId, cancellationToken);

            if (studentIds.Any())
            {
                var template = SectionNotifications.Updated;
                var message = string.Format(template.Description, notification.Title);

                await _notificationService.SendToUsersAsync(studentIds, template.Title, message, cancellationToken);
            }
        }
    }
}
