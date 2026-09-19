using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetStudentSubscriptionStreamImage
{
    public sealed class GetStudentSubscriptionStreamImageQueryHandler : BaseService,IQueryHandler<GetStudentSubscriptionStreamImageQuery, FileStream>
    {
        private readonly IStudentSubscriptionRepositry _subscriptionRepo;
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public GetStudentSubscriptionStreamImageQueryHandler(IHttpContextAccessor httpContextAccessor, IStudentSubscriptionRepositry subscriptionRepo, IFileService fileService, IUserRepository userRepository) : base(httpContextAccessor)
        {
            _subscriptionRepo = subscriptionRepo;
            _fileService = fileService;
            _userRepository = userRepository;
        }

        public async Task<Result<FileStream>> Handle(GetStudentSubscriptionStreamImageQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<FileStream>(UserErrors.NotFound);
            if(user.Role.notType != Domain.Roles.NotType.Student)
            {
                return Result.Failure<FileStream>(UserErrors.Unauthorized);
            }
            var subscription = await _subscriptionRepo.GetByIdAsync(request.StudentSubscriptionId, cancellationToken);
            if (subscription is null || string.IsNullOrEmpty(subscription.ReceiptImageUrl.Value))
            {
                return Result.Failure<FileStream>(StudentSubscriptionErrors.NotFound);
            }
            try
            {
                var stream = _fileService.GetImageProvider(subscription.ReceiptImageUrl.Value);
                return Result.Success(stream);
            }
            catch (FileNotFoundException)
            {
                return Result.Failure<FileStream>(LessonsErrors.FileNotFoundOnServer);
            }

        }
    }
}
