using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Notifications;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Teachers;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Courses.Commands.AddCourse
{
    public sealed class AddCourseCommandHandler : BaseService,ICommandHandler<AddCourseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassesRepositry _classesRepository;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;

        public AddCourseCommandHandler(IUnitOfWork unitOfWork,
            ICourseRepository courseRepository,
            ITeacherRepository teacherRepository,
            IClassesRepositry classesRepository,
           IHttpContextAccessor httpContextAccessor,
           IFileService fileService,
           INotificationService notificationService) : base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _classesRepository = classesRepository;
            _fileService = fileService;
            _notificationService = notificationService;
        }

        public async Task<Result<Guid>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var teacher = await _teacherRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (teacher is null)
                return Result.Failure<Guid>(TeacherErrors.NotFound);
            var classes = await _classesRepository.GetClassesByNameAsync(new ClassesName(request.ClassroomName), cancellationToken);
            if (classes is null)
                return Result.Failure<Guid>(ClassesErrors.NotFound);
            string imageUrl = await _fileService.UploadImageAsync(request.ImageUrl, "courses", cancellationToken);
            var course = Course.Create(
                new CourseName(request.Title),
                new ImageUrl(imageUrl),
                new Description(request.Description),
                new Price(request.Price),
                classes.Id,
                teacher.Id
            );
           
            await _courseRepository.AddAsync(course, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(course.Id);
        }
    }
}
