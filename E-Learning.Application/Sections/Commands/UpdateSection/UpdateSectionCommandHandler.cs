

using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Sections;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Sections.Commands.UpdateSection
{
    public sealed class UpdateSectionCommandHandler : BaseService, ICommandHandler<UpdateSectionCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISectionRepository _sectionRepository;

        public UpdateSectionCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork, 
            ISectionRepository sectionRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _sectionRepository = sectionRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var section = await _sectionRepository.GetByIdAsync(request.Id , cancellationToken);
            if(section is null)
                return Result.Failure<Guid>(SectionErrors.NotFound);
            if (user.Id != section?.Course.TeacherId && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            section.UpdateSeciton(
               new SectionTitle(request.Title),
               new Domain.Shared.Price(request.Price)
            );
            await _sectionRepository.UpdateAsync(section, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(section.Id);
        }
    }
}
