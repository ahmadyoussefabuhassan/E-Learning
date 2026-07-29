using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllInvtensivesSubscriptionsByStudent
{
    public sealed class GetAllInvtensivesSubscriptionsByStudentQueryHandler : BaseService , IQueryHandler<GetAllInvtensivesSubscriptionsByStudentQuery, IEnumerable<InvtensiveResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public GetAllInvtensivesSubscriptionsByStudentQueryHandler(IUserRepository userRepository, IStudentSubscriptionRepositry studentSubscriptionRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<IEnumerable<InvtensiveResponse>>> Handle(GetAllInvtensivesSubscriptionsByStudentQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<IEnumerable<InvtensiveResponse>>(UserErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<IEnumerable<InvtensiveResponse>>(UserErrors.Unauthorized);
            var invtensives = await _studentSubscriptionRepositry.GetAllInvtensivesSubscribersAsync(user.Id, cancellationToken);
            if(!invtensives.Any())
                return Result.Success<IEnumerable<InvtensiveResponse>>(Enumerable.Empty<InvtensiveResponse>());
            var response = invtensives.Select(i => new InvtensiveResponse(
                i.Id,
                i.Title.Value,
                i.Description.Value,
                i.Price.Value,
                i.IsLocked
            ));
            return Result.Success(response);
        }
    }
}
