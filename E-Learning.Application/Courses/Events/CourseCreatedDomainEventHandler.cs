using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses.Events;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using MediatR;

namespace E_Learning.Application.Courses.Events
{
    public sealed class CourseCreatedDomainEventHandler : INotificationHandler<CourseCreatedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;

        public CourseCreatedDomainEventHandler(INotificationService notificationService, IUserRepository userRepository)
        {
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task Handle(CourseCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var teacher = await _userRepository.GetByIdAsync(notification.TeacherId, cancellationToken);
            var body = string.Format(
              CourseNotifications.CourseCreated.Description,
              notification.Name,  // {0}
              teacher?.FullName?.Value ?? "أستاذنا القدير"    // {1}
            );
            await _notificationService.SendToAllAsync(CourseNotifications.CourseCreated.Title, body, cancellationToken);
        }
    }
}
