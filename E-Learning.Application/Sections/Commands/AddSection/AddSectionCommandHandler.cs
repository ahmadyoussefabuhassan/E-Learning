using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Shared;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Sections.Commands.AddSection
{
    public sealed class AddSectionCommandHandler : BaseService, ICommandHandler<AddSectionCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISectionRepository _sectionRepository;

        public AddSectionCommandHandler(IUserRepository userRepository,
            ICourseRepository courseRepository,
            IUnitOfWork unitOfWork, 
            ISectionRepository sectionRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
            _sectionRepository = sectionRepository;
        }

        public async Task<Result<Guid>> Handle(AddSectionCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId , cancellationToken);
            if(user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var cuorse = await _courseRepository.GetByIdAsync(request.CourseId , cancellationToken);
            if(cuorse?.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var section = Section.Create(
                new SectionTitle(request.Title),
                new Price(request.Price),
                cuorse.Id
            );
            await _sectionRepository.AddAsync(section , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(section.Id);
        }
    }
}
