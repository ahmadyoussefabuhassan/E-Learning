using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Invtensives.Commands.AddInvtensive
{
    public sealed class AddInvtensiveCommandHandler : BaseService, ICommandHandler<AddInvtensiveCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IInvtensivesRepositry _invtensivesRepositry;

        public AddInvtensiveCommandHandler(IUnitOfWork unitOfWork,
            IUserRepository userRepository, 
            ICourseRepository courseRepository,
            IInvtensivesRepositry invtensivesRepositry,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _invtensivesRepositry = invtensivesRepositry;
        }

        public async Task<Result<Guid>> Handle(AddInvtensiveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if(course is null)
                return Result.Failure<Guid>(CourseErrors.NotFound);
            if(course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var invtensive = Domain.Invtensives.Invtensives.Create(
                new InvtensivesTitle(request.Title),
                new Domain.Shared.Description(request.Description),
                new Domain.Shared.Price(request.Price),
                course.Id
            );
            await _invtensivesRepositry.AddAsync(invtensive , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(invtensive.Id);
        }
    }
}
