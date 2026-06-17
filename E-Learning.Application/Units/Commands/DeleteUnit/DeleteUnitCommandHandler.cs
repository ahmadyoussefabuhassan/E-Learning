using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.Units;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Units.Commands.DeleteUnit
{
    public sealed class DeleteUnitCommandHandler : BaseService,ICommandHandler<DeleteUnitCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitRepository _unitRepository;
        private readonly IFileService _fileService;
        private readonly ILessonRepository _lessonRepository;

        public DeleteUnitCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IUnitRepository unitRepository,
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService,
            ILessonRepository lessonRepository) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
            _fileService = fileService;
            _lessonRepository = lessonRepository;
        }

        public async Task<Result<bool>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            Guid currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            var unit = await _unitRepository.GetByIdAsync(request.Id, cancellationToken);
            if (unit is null)
                return Result.Failure<bool>(UnitsErrors.NotFound);
            if (user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<bool>(UserErrors.Unauthorized);
            var lessons = await _lessonRepository.GetLessonsByUnitAsync(unit.Id, cancellationToken);
            if(!lessons.Any() && lessons is not null)
            {
                foreach (var lesson in lessons)
                {
                    if (!string.IsNullOrEmpty(lesson.URL?.Value))
                    {
                        _fileService.DeleteVideo(lesson.URL.Value);
                    }
                    await _lessonRepository.DeleteAsync(lesson.Id, cancellationToken);
                }
            }
            await _unitRepository.DeleteAsync(unit.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
