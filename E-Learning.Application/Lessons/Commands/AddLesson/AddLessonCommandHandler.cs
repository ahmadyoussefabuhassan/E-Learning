using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.Units;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Commands.AddLesson
{
    public sealed class AddLessonCommandHandler : BaseService, ICommandHandler<AddLessonCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileService _fileService;

        public AddLessonCommandHandler(IUnitOfWork unitOfWork,
            IUnitRepository unitRepository,
            IUserRepository userRepository,
            ILessonRepository lessonRepository,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
            _userRepository = userRepository;
            _lessonRepository = lessonRepository;
            _fileService = fileService;
        }

        public async Task<Result<Guid>> Handle(AddLessonCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var unit = await _unitRepository.GetByIdAsync(request.unitId , cancellationToken);
            if (unit is null)
                return Result.Failure<Guid>(UnitsErrors.NotFound);
            if (unit.Section.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var vidourl = await _fileService.UploadVideoAsync(request.VidoUrl, "lossons", cancellationToken);
            var lesson = Lesson.Create(
                new LessonTitle(request.Title),
                new URL(vidourl),
                new TitleUrl(request.TitleUrl),
                unit.Id
            );
            await _lessonRepository.AddAsync( lesson , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(lesson.Id);
        }
    }
}
