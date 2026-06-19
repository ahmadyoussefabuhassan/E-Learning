using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.Notifications.Queries.GetNotificationById
{
    public sealed class GetNotificationByIdQueryHandler : BaseService, IQueryHandler<GetNotificationByIdQuery, NotificationResponse>
    {
        private readonly INotificationRepositry _notificationRepository;

        public GetNotificationByIdQueryHandler(
            INotificationRepositry notificationRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Result<NotificationResponse>> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (notification == null)
                return Result.Failure<NotificationResponse>(NotificationErrors.NotFound);

            if (notification.UserId != UserId)
                return Result.Failure<NotificationResponse>(UserErrors.Unauthorized);

            var response = new NotificationResponse(
                notification.Id,
                notification.Title.Value,
                notification.Message.Value,
                notification.CreatedAt,
                notification.IsRead
            );

            return Result.Success(response);
        }
    }
}
