using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Notification;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Notifications.Commands.DeleteAllNotifications
{
    public sealed class DeleteAllNotificationsCommandHandler : BaseService, ICommandHandler<DeleteAllNotificationsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepositry _notificationRepository;
        private readonly IUserRepository _userRepository;
        public DeleteAllNotificationsCommandHandler(IHttpContextAccessor httpContextAccessor, 
            IUnitOfWork unitOfWork,
            INotificationRepositry notificationRepository,
            IUserRepository userRepository) : base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteAllNotificationsCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure(UserErrors.NotFound);
           await _notificationRepository.DeleteAllNotificationByUserIdAsync(user.Id, cancellationToken);
           await _unitOfWork.SaveChangesAsync(cancellationToken);
           return Result.Success();
        }
    }
}
