using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Invtensives.Commands.UpdateInvtensive
{
    public sealed class UpdateInvtensiveCommandHandler : BaseService, ICommandHandler<UpdateInvtensiveCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IInvtensivesRepositry _invtensivesRepositry;

        public UpdateInvtensiveCommandHandler(IUnitOfWork unitOfWork, 
            IUserRepository userRepository, 
            IInvtensivesRepositry invtensivesRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _invtensivesRepositry = invtensivesRepositry;
        }

        public async Task<Result<Guid>> Handle(UpdateInvtensiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var invtensive = await _invtensivesRepositry.GetByIdAsync(request.Id , cancellationToken);
            if(invtensive is null)
                return Result.Failure<Guid>(InvtensivesErrors.NotFound);
            if (invtensive.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            invtensive.UpdateInvtensives(
                new InvtensivesTitle(request.Title),
                new Domain.Shared.Description(request.Description),
                new Domain.Shared.Price(request.Price)
            );
            await _invtensivesRepositry.UpdateAsync( invtensive , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(invtensive.Id);
        }
    }
}
