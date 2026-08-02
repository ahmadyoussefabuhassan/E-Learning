using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.Students;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegiterExamExplanation
{
    public sealed class RegiterExamExplanationCommandHandler : BaseService, ICommandHandler<RegiterExamExplanationCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExamExplanationRepository _examExplanationRepository;
        private readonly IFileService _fileService;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public RegiterExamExplanationCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IExamExplanationRepository examExplanationRepository,
            IFileService fileService,
            IStudentSubscriptionRepositry studentSubscriptionRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _examExplanationRepository = examExplanationRepository;
            _fileService = fileService;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<Guid>> Handle(RegiterExamExplanationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(StudentErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var exam = await _examExplanationRepository.GetByIdAsync(request.targetId, cancellationToken);
            if(exam is null)
                return Result.Failure<Guid>(ExamExplanationsErrors.NotFound);
            bool alreadyRequested = await _studentSubscriptionRepositry.IsAlreadySubscribedAsync(user.Id, exam.Id, cancellationToken);
            if (alreadyRequested)
                return Result.Failure<Guid>(StudentSubscriptionErrors.Duplicate);
            string Filepath = await _fileService.UploadImageAsync(request.ReceiptImageUrl, "StudentSubscriptions", cancellationToken);
            var studentSubscription = StudentSubscription.Create(
                user.Id,
                exam.Id,
                new TargetType(TargetTypes.ExamExplanation.ToArabicString()),
                new ReceiptImageUrl(Filepath),
                SubscriptionStatus.Pending,
                new PriceAtPurchase(exam.Price.Value)
            );
            await _studentSubscriptionRepositry.AddAsync(studentSubscription, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(studentSubscription.Id);

        }
    }
}
