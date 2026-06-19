using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.Notifications.Queries.GetMyNotifications
{
    public sealed class GetMyNotificationsQueryHandler : BaseService, IQueryHandler<GetMyNotificationsQuery, IEnumerable<NotificationResponse>>
    {
        private readonly INotificationRepositry _notificationRepository;

        public GetMyNotificationsQueryHandler(
            INotificationRepositry notificationRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<IEnumerable<NotificationResponse>>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = UserId;

            var notifications = await _notificationRepository.GetByUserIdAsync(currentUserId, cancellationToken);

            if (notifications == null || !notifications.Any())
                return Result.Success(Enumerable.Empty<NotificationResponse>());

            var response = notifications.Select(n => new NotificationResponse(
                n.Id,
                n.Title.Value,
                n.Message.Value,
                n.CreatedAt,
                n.IsRead 
            ));

            return Result.Success(response);
        }
    }
}
