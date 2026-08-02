using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Students;
using E_Learning.Domain.StudentSubscription;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.StudentSubscriptions.Commands.RegisterSection
{
    public sealed class RegisterSectionCommandHandler : BaseService, ICommandHandler<RegisterSectionCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISectionRepository _sectionRepository;
        private readonly IFileService _fileService;
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public RegisterSectionCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork, 
            ISectionRepository sectionRepository,
            IFileService fileService, 
            IStudentSubscriptionRepositry studentSubscriptionRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _sectionRepository = sectionRepository;
            _fileService = fileService;
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<Guid>> Handle(RegisterSectionCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(StudentErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Student)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var section = await _sectionRepository.GetByIdAsync(request.targetId,cancellationToken);
            if(section is null)
                return Result.Failure<Guid>(SectionErrors.NotFound);
            bool alreadyRequested = await _studentSubscriptionRepositry.IsAlreadySubscribedAsync(user.Id, section.Id, cancellationToken);
            if (alreadyRequested)
                return Result.Failure<Guid>(StudentSubscriptionErrors.Duplicate);
            string Filepath = await _fileService.UploadImageAsync(request.ReceiptImageUrl, "StudentSubscriptions", cancellationToken);
            var studentSubscription = StudentSubscription.Create(
                user.Id,
                section.Id,
                new TargetType(TargetTypes.Section.ToArabicString()),
                new ReceiptImageUrl(Filepath),
                SubscriptionStatus.Pending,
                new PriceAtPurchase(section.Price.Value)
            );
            await _studentSubscriptionRepositry.AddAsync(studentSubscription, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(studentSubscription.Id);
        }
    }
}
