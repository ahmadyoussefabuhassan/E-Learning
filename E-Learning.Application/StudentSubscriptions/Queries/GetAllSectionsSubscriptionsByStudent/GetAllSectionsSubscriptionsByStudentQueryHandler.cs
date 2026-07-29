using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllSectionsSubscriptionsByStudent
{
    public sealed class GetAllSectionsSubscriptionsByStudentQueryHandler : BaseService, IQueryHandler<GetAllSectionsSubscriptionsByStudentQuery, IEnumerable<SectionResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public GetAllSectionsSubscriptionsByStudentQueryHandler(IUserRepository userRepository, IStudentSubscriptionRepositry studentSubscriptionRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<IEnumerable<SectionResponse>>> Handle(GetAllSectionsSubscriptionsByStudentQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<IEnumerable<SectionResponse>>(UserErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<IEnumerable<SectionResponse>>(UserErrors.Unauthorized);
            var sections = await _studentSubscriptionRepositry.GetAllSectionSubscribersAsync(user.Id, cancellationToken);
            if (sections is null || !sections.Any())
                return Result.Success<IEnumerable<SectionResponse>>(Enumerable.Empty<SectionResponse>());
            var response = sections.Select(s => new SectionResponse(
                s.Id,
                s.SectionTitle.Value,
                s.Price.Value,
                s.IsLocked
            ));
            return Result.Success(response);
        }
    }
}
