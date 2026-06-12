using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Courses.Events;
using E_Learning.Domain.StudentSubscription;
using MediatR;

namespace E_Learning.Application.Courses.Events
{
    public sealed class CourseUpdatedDomainEventHandler : INotificationHandler<CourseUpdatedDomainEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public CourseUpdatedDomainEventHandler(INotificationService notificationService, IStudentSubscriptionRepositry studentSubscriptionRepositry)
        {
            _notificationService = notificationService;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task Handle(CourseUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var students =  await _studentSubscriptionRepositry.GetSubscribedStudentIdsAsync(notification.Id, cancellationToken);
            if (!students.Any())
                return;
            var message = string.Format(CourseNotifications.Updated.Description, notification.Name);
            await _notificationService.SendToUsersAsync(students, CourseNotifications.Updated.Title, message, cancellationToken);
        }
    }
}
