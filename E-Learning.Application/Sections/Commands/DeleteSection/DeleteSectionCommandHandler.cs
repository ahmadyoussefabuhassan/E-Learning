using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Sections;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Sections.Commands.DeleteSection
{
    public sealed class DeleteSectionCommandHandler : BaseService, ICommandHandler<DeleteSectionCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISectionRepository _sectionRepository;

        public DeleteSectionCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            ISectionRepository sectionRepository) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _sectionRepository = sectionRepository;
        }

        public async Task<Result<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            var section = await _sectionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (section is null)
                return Result.Failure<bool>(SectionErrors.NotFound);
            if (user.Id != section?.Course.TeacherId && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<bool>(UserErrors.Unauthorized);
            bool hasDe = await _sectionRepository.HasRelatedDataAsync(request.Id, cancellationToken);
            if (hasDe)
                return Result.Failure<bool>(SectionErrors.HasRelatedData);
            await _sectionRepository.DeleteAsync(section.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);

        }
    }
}
