using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Application.Notifications.Commands.SendBroadcastNotification.E_Learning.Application.Notifications.Commands.SendBroadcastNotification;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification;
using E_Learning.Domain.Roles;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Notifications.Commands.SendBroadcastNotification
{
    public sealed class SendBroadcastNotificationCommandHandler : BaseService, ICommandHandler<SendBroadcastNotificationCommand>
    {
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;

        public SendBroadcastNotificationCommandHandler(
            INotificationService notificationService,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _notificationService = notificationService;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(SendBroadcastNotificationCommand request, CancellationToken cancellationToken)
        {

            List<Guid> targetIds = new();
            switch (request.Audience)
            {
                case NotificationAudience.StudentsOnly:
                    targetIds = await _userRepository.GetUserIdsByRoleAsync(NotType.Student, cancellationToken);
                    break;

                case NotificationAudience.TeachersOnly:
                    targetIds = await _userRepository.GetUserIdsByRoleAsync(NotType.Teacher, cancellationToken);
                    break;

                case NotificationAudience.All:
                    targetIds = await _userRepository.GetAllUsersExceptAdminAsync(cancellationToken);
                    break;
            }

            if (!targetIds.Any())
                return Result.Success();

            await _notificationService.SendToUsersAsync(targetIds, request.Title, request.Message, cancellationToken);

            return Result.Success();
        }
    }
}
