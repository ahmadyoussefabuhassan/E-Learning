using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;

namespace E_Learning.Application.Courses.Commands.DeleteCourse
{
    public sealed class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IFileService _fileService;

        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork, ICourseRepository courseRepository, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _fileService = fileService;
        }

        public async Task<Result<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if(course is null)
                return Result.Failure<bool>(CourseErrors.NotFound);
            if (!string.IsNullOrEmpty(course.ImageUrl?.Value))
                _fileService.DeleteImage(course.ImageUrl.Value);
            await _courseRepository.DeleteAsync(course.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
