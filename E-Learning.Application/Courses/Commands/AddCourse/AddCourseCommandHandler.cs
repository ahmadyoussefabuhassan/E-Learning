using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Shared;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Courses.Commands.AddCourse
{
    public sealed class AddCourseCommandHandler : BaseService,ICommandHandler<AddCourseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClassesRepositry _classesRepository;
        private readonly IFileService _fileService;

        public AddCourseCommandHandler(IUnitOfWork unitOfWork,
            ICourseRepository courseRepository,
            IUserRepository userRepository,
            IClassesRepositry classesRepository,
           IHttpContextAccessor httpContextAccessor,
           IFileService fileService) : base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _classesRepository = classesRepository;
            _fileService = fileService;
        }

        public async Task<Result<Guid>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErorrs.NotFound);
            if (user.Role.notType != NotType.Teacher && user.Role.notType != NotType.Admin)
                return Result.Failure<Guid>(UserErorrs.Unauthorized);
            var classes = await _classesRepository.GetClassesByNameAsync(new ClassesName(request.ClassroomName), cancellationToken);
            if (classes is null)
                return Result.Failure<Guid>(ClassesErrors.NotFound);
            string imageUrl = await _fileService.UploadImageAsync(request.ImageUrl, "courses", cancellationToken);
            var course = Course.Create(
                new CourseName(request.Title),
                new Domain.Courses.ImageUrl(imageUrl),
                new Description(request.Description),
                new Price(request.Price),
                classes.Id,
                user.Id
            );
           
            await _courseRepository.AddAsync(course, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(course.Id);
        }
    }
}
