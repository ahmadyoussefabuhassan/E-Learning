using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Students;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterCourse
{
    public sealed class RegisterCourseCommandHandler : BaseService, ICommandHandler<RegisterCourseCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IFileService _fileService;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public RegisterCourseCommandHandler(
            IUserRepository userRepository, 
            IUnitOfWork unitOfWork, 
            ICourseRepository courseRepository,
            IFileService fileService, 
            IStudentSubscriptionRepositry studentSubscriptionRepositry,
            IHttpContextAccessor httpContextAccessor) :base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _fileService = fileService;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<Guid>> Handle(RegisterCourseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId , cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(StudentErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var course = await _courseRepository.GetByIdAsync(request.targetId , cancellationToken);
            if(course is null)
                return Result.Failure<Guid>(CourseErrors.NotFound);
            bool alreadyRequested = await _studentSubscriptionRepositry.IsAlreadySubscribedAsync(user.Id, course.Id, cancellationToken);
            if (alreadyRequested)
                return Result.Failure<Guid>(StudentSubscriptionErrors.Duplicate);
            string Filepath = await _fileService.UploadImageAsync(request.ReceiptImageUrl, "StudentSubscriptions", cancellationToken);
            var studentSubscription = StudentSubscription.Create(
                user.Id,
                course.Id,
                new TargetType(TargetTypes.Course.ToArabicString()),
                new ReceiptImageUrl(Filepath),
                SubscriptionStatus.Pending,
                new PriceAtPurchase(course.Price.Value)
            );
            await _studentSubscriptionRepositry.AddAsync(studentSubscription, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(studentSubscription.Id);
        }
    }
}
