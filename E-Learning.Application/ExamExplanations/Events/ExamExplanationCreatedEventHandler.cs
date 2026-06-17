using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamExplanations.Events;
using E_Learning.Domain.StudentSubscription;
using MediatR;

namespace E_Learning.Application.ExamExplanations.Events
{
    public sealed class ExamExplanationCreatedEventHandler
        : INotificationHandler<ExamExplanationCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IStudentSubscriptionRepositry _subscriptionRepo;

        public ExamExplanationCreatedEventHandler(
            INotificationService notificationService,
            IStudentSubscriptionRepositry subscriptionRepo)
        {
            _notificationService = notificationService;
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task Handle(ExamExplanationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var allStudentIds = await _subscriptionRepo.GetAllStudentIdsAsync(cancellationToken);

            if (allStudentIds != null && allStudentIds.Any())
            {
                var template = ExamNotifications.ExamExplanationCreated;
                var message = string.Format(template.Description, notification.Title);
                await _notificationService.SendToUsersAsync(allStudentIds, template.Title, message, cancellationToken);
            }
        }
    }
}