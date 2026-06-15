using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Units;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Units.Commands.UpdateUnit
{
    public sealed class UpdateUnitCommandHandler : BaseService, ICommandHandler<UpdateUnitCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IUnitRepository _unitRepository;

        public UpdateUnitCommandHandler(IUnitOfWork unitOfWork, 
            IUserRepository userRepository,
            IUnitRepository unitRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _unitRepository = unitRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var unit = await _unitRepository.GetByIdAsync(request.Id , cancellationToken);
            if(unit is null)
                return Result.Failure<Guid>(UnitsErrors.NotFound);
            if(unit.Section.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            unit.UpdateUnit(
              new UnitTitle(request.Title),
              new Domain.Shared.Description(request.Description)
            );
            await _unitRepository.UpdateAsync(unit , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(unit.Id);
        }
    }
}
