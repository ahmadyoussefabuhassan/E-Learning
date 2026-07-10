using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Notifications.Commands.DeleteNotification
{
    public sealed class DeleteNotificationCommandHandler : BaseService, ICommandHandler<DeleteNotificationCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepositry _notificationRepositry;
        public DeleteNotificationCommandHandler(IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork,
            INotificationRepositry notificationRepositry) : base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _notificationRepositry = notificationRepositry;
        }

        public async Task<Result> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepositry.GetByIdAsync(request.NotificationId, cancellationToken);
            if (notification is null)
                return Result.Failure(NotificationErrors.NotFound);
            if (notification.UserId != UserId)
                return Result.Failure(UserErrors.Unauthorized);
            await _notificationRepositry.DeleteAsync(notification.Id , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
