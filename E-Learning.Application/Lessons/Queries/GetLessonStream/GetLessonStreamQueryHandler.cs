using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Queries.GetLessonStream
{
    public sealed class GetLessonStreamQueryHandler : BaseService , IQueryHandler<GetLessonStreamQuery, FileStream>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public GetLessonStreamQueryHandler(ILessonRepository lessonRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : base(httpContextAccessor)
        {
            _lessonRepository = lessonRepository;
            _fileService = fileService;
            _userRepository = userRepository;
        }

        public async Task<Result<FileStream>> Handle(GetLessonStreamQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<FileStream>(UserErrors.NotFound);
            var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken);
            if (lesson == null || string.IsNullOrEmpty(lesson.URL?.Value))
                return Result.Failure<FileStream>(LessonsErrors.NotFound);
            if(user.Role.notType == Domain.Roles.NotType.Student)
            {
                if (lesson.Unit.Section.IsLocked || lesson.Unit.Section.Course.IsLocked)
                    return Result.Failure<FileStream>(LessonsErrors.AccessDenied);
            }
         

            try
            {
                var stream = _fileService.GetVideoProvider(lesson.URL.Value);
                return Result.Success(stream);
            }
            catch (FileNotFoundException)
            {
                return Result.Failure<FileStream>(LessonsErrors.FileNotFoundOnServer);
            }
        }
    }
}
