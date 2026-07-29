using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllExamExplanationsSubscriptionsByStudent
{
    public sealed class GetAllExamExplanationsSubscriptionsByStudentQueryHandler : BaseService, IQueryHandler<GetAllExamExplanationsSubscriptionsByStudentQuery, IEnumerable<ExamExplanationResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public GetAllExamExplanationsSubscriptionsByStudentQueryHandler(IUserRepository userRepository, IStudentSubscriptionRepositry studentSubscriptionRepositry ,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<IEnumerable<ExamExplanationResponse>>> Handle(GetAllExamExplanationsSubscriptionsByStudentQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<IEnumerable<ExamExplanationResponse>>(UserErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<IEnumerable<ExamExplanationResponse>>(UserErrors.Unauthorized);
            var examExplanations = await _studentSubscriptionRepositry.GetAllExamExplanationSubscribersAsync(user.Id, cancellationToken);
            if(!examExplanations.Any())
                return Result.Success<IEnumerable<ExamExplanationResponse>>(Enumerable.Empty<ExamExplanationResponse>());
            var response = examExplanations.Select(e => new ExamExplanationResponse(
                e.Id,
                e.Title.Value,
                e.Description.Value,
                e.Price.Value,
                e.IsLocked
            ));
            return Result.Success(response);
        }
    }
}
