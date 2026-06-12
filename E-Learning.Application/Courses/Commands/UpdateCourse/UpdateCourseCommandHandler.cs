using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Shared;

namespace E_Learning.Application.Courses.Commands.UpdateCourse
{
    public sealed class UpdateCourseCommandHandler :  ICommandHandler<UpdateCourseCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IClassesRepositry _classesRepositry;
        private readonly IFileService _fileService;

        public UpdateCourseCommandHandler(IUnitOfWork unitOfWork,
            ICourseRepository courseRepository,
            IClassesRepositry classesRepositry,
            IFileService fileService)
             
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _classesRepositry = classesRepositry;
            _fileService = fileService;
        }

        public async Task<Result<Guid>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.CourseId, cancellationToken);
            if(course is null)
                return Result.Failure<Guid>(CourseErrors.NotFound);
            var classes = await _classesRepositry.GetClassesByNameAsync(new ClassesName(request.ClassroomName),
                 cancellationToken);
            if (classes is null)
                return Result.Failure<Guid>(ClassesErrors.NotFound);
            string image = course.ImageUrl.Value;
            if (request.ImageFile is not null)
            {
                _fileService.DeleteImage(course.ImageUrl.Value);
                image = await _fileService.UploadImageAsync(request.ImageFile, "courses", cancellationToken);
            }
            course.Update(
                new CourseName(request.Title), 
                new ImageUrl(image), 
                new Description(request.Description),
                new Price(request.Price), 
                classes.Id
            );
            await _courseRepository.UpdateAsync(course , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(course.Id);

        }
    }
}
