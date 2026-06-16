using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Sections;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.Units;
using E_Learning.Domain.Units.Event;
using MediatR;

namespace E_Learning.Application.Units.Events
{
    public sealed class UnitCreatedDomainEventHandler : INotificationHandler<UnitCreatedDomainEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IStudentSubscriptionRepositry _subscriptionRepo;
        private readonly ISectionRepository _sectionRepository;

        public UnitCreatedDomainEventHandler(
            INotificationService notificationService,
            IStudentSubscriptionRepositry subscriptionRepo,
            ISectionRepository sectionRepository)
        {
            _notificationService = notificationService;
            _subscriptionRepo = subscriptionRepo;
            _sectionRepository = sectionRepository;
        }
        public async Task Handle(UnitCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(notification.SectionId, cancellationToken);
            var studentIds = await _subscriptionRepo.GetSectionOrCourseSubscribersAsync(
             notification.SectionId,
             section.CourseId,  
             cancellationToken
            );
            if (studentIds.Any())
            {
                var template = UnitNotifications.UnitCreated;
                var message = string.Format(template.Description, notification.Title);
                await _notificationService.SendToUsersAsync(studentIds, template.Title, message, cancellationToken);
            }
        }
    }
}
