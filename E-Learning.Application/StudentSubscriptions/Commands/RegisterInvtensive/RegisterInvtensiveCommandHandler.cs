using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.Students;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterInvtensive
{
    public sealed class RegisterInvtensiveCommandHandler : BaseService , ICommandHandler<RegisterInvtensiveCommand , Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvtensivesRepositry _invtensivesRepositry;
        private readonly IFileService _fileService;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public RegisterInvtensiveCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork, 
            IInvtensivesRepositry invtensivesRepositry,
            IFileService fileService, 
            IStudentSubscriptionRepositry studentSubscriptionRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _invtensivesRepositry = invtensivesRepositry;
            _fileService = fileService;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<Guid>> Handle(RegisterInvtensiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(StudentErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var invtensive = await _invtensivesRepositry.GetByIdAsync(request.targetId, cancellationToken);
            if (invtensive is null)
                return Result.Failure<Guid>(InvtensivesErrors.NotFound);
            bool alreadyRequested = await _studentSubscriptionRepositry.IsAlreadySubscribedAsync(user.Id, invtensive.Id, cancellationToken);
            if (alreadyRequested)
                return Result.Failure<Guid>(StudentSubscriptionErrors.Duplicate);
            string Filepath = await _fileService.UploadImageAsync(request.ReceiptImageUrl, "StudentSubscriptions", cancellationToken);
            var studentSubscription = StudentSubscription.Create(
                user.Id,
                invtensive.Id,
                new TargetType(TargetTypes.Invtensive.ToArabicString()+"\t"+invtensive.Title.Value),
                new ReceiptImageUrl(Filepath),
                SubscriptionStatus.Pending,
                new PriceAtPurchase(invtensive.Price.Value)
            );
            await _studentSubscriptionRepositry.AddAsync(studentSubscription, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(studentSubscription.Id);

        }
    }
}
