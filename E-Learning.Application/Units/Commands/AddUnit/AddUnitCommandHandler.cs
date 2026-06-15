using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Units;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Units.Commands.AddUnit
{
    public sealed class AddUnitCommandHandler : BaseService, ICommandHandler<AddUnitCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitRepository _unitRepository;

        public AddUnitCommandHandler(IUserRepository userRepository, 
            ISectionRepository sectionRepository,
            IUnitOfWork unitOfWork,
            IUnitRepository unitRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _sectionRepository = sectionRepository;
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
        }

        public async Task<Result<Guid>> Handle(AddUnitCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var section = await _sectionRepository.GetByIdAsync(request.sectionId, cancellationToken);
            if (section is null)
                return Result.Failure<Guid>(SectionErrors.NotFound);
            if (section.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var unit = Unit.Create(
                new UnitTitle(request.Title),
                new Domain.Shared.Description(request.Description),
                section.Id
            ); 
            await _unitRepository.AddAsync(unit, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(unit.Id);
        }
    }
}
