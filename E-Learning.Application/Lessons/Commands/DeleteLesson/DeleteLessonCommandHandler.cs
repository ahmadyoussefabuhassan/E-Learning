using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Commands.DeleteLesson
{
    public sealed class DeleteLessonCommandHandler :BaseService, ICommandHandler<DeleteLessonCommand, bool>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public DeleteLessonCommandHandler(
            ILessonRepository lessonRepository,
            IFileService fileService,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _lessonRepository = lessonRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<Result<bool>> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);
            if (lesson is null)
                return Result.Failure<bool>(LessonsErrors.NotFound);
            if (lesson.Unit.Section.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<bool>(UserErrors.Unauthorized);
            if (!string.IsNullOrEmpty(lesson.URL?.Value))
            {
                _fileService.DeleteVideo(lesson.URL.Value);
            }
            await _lessonRepository.DeleteAsync(lesson.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
