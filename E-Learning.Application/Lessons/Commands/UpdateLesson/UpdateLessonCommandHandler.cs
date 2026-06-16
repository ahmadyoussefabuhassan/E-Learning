using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Commands.UpdateLesson
{
    public sealed class UpdateLessonCommandHandler : BaseService, ICommandHandler<UpdateLessonCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileService _fileService;

        public UpdateLessonCommandHandler(IUnitOfWork unitOfWork,
            IUserRepository userRepository, 
            ILessonRepository lessonRepository,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _lessonRepository = lessonRepository;
            _fileService = fileService;
        }

        public async Task<Result<Guid>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var lesson =  await _lessonRepository.GetByIdAsync(request.Id , cancellationToken);
            if (lesson?.Unit.Section.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            string? vido = lesson.URL.Value;
            if (request.VidoUrl is not null)
            {
                if (!string.IsNullOrEmpty(lesson.URL?.Value))
                     _fileService.DeleteVideo(lesson.URL.Value);
                vido = await _fileService.UploadVideoAsync(request.VidoUrl , "lessons" , cancellationToken);
            }
            lesson.UpdateLesson(
                new LessonTitle(request.Title),
                new URL(vido),
                new TitleUrl(request.TitleUrl)
            );
            await _lessonRepository.UpdateAsync(lesson , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(lesson.Id);
        }
    }
}
