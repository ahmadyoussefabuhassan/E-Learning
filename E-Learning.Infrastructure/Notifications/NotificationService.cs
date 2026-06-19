using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Notifications
{
    public sealed class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationRepositry _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public NotificationService(
            IHubContext<NotificationHub> hubContext,
            INotificationRepositry notificationRepository,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task SendToAllAsync(string title, string message, CancellationToken cancellation = default)
        {
            var userIds = await _userRepository.GetAllUserIdsAsync(cancellation);

            foreach (var userId in userIds)
            {
                var notification = Notification.Create(
                    userId,
                    new Message(message),
                    new Title(title),
                    new UrlRedirect("")
                );
                await _notificationRepository.AddAsync(notification, cancellation);
            }

            await _unitOfWork.SaveChangesAsync(cancellation);

            await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            }, cancellation);
        }

        public async Task SendToUserAsync(Guid userId, string title, string message, CancellationToken cancellation = default)
        {
            var notification = Notification.Create(
                userId,
                new Message(message),
                new Title(title),
                new UrlRedirect("")
            );

            await _notificationRepository.AddAsync(notification, cancellation);
            await _unitOfWork.SaveChangesAsync(cancellation);

            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            }, cancellation);
        }

        public async Task SendToUsersAsync(IEnumerable<Guid> userIds, string title, string message, CancellationToken cancellation = default)
        {
            foreach (var userId in userIds)
            {
                var notification = Notification.Create(
                    userId,
                    new Message(message),
                    new Title(title),
                    new UrlRedirect("")
                );
                await _notificationRepository.AddAsync(notification, cancellation);
            }

            await _unitOfWork.SaveChangesAsync(cancellation);

            var userIdsStrings = userIds.Select(id => id.ToString()).ToList();
            await _hubContext.Clients.Users(userIdsStrings).SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            }, cancellation);
        }

        public async Task SendToGroupAsync(string groupName, string title, string message, CancellationToken cancellation = default)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", new
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            }, cancellation);
        }
    }
}