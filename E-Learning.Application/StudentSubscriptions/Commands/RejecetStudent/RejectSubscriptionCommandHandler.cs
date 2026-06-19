using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.StudentSubscriptions.Commands.RejecetStudent
{
    public sealed class RejectSubscriptionCommandHandler : BaseService, ICommandHandler<RejectSubscriptionCommand, Guid>
    {
        private readonly IStudentSubscriptionRepositry _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RejectSubscriptionCommandHandler(
            IStudentSubscriptionRepositry subscriptionRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RejectSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user == null || user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);

            var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId, cancellationToken);
            if (subscription == null)
                return Result.Failure<Guid>(StudentSubscriptionErrors.NotFound);

            subscription.Reject();
            await _subscriptionRepository.UpdateAsync(subscription , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(subscription.Id);
        }
    }
}
