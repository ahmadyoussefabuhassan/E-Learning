using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;

namespace E_Learning.Application.Lessons.Queries.GetLessonStream
{
    public sealed class GetLessonStreamQueryHandler : IQueryHandler<GetLessonStreamQuery, FileStream>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IFileService _fileService;

        public GetLessonStreamQueryHandler(ILessonRepository lessonRepository, IFileService fileService)
        {
            _lessonRepository = lessonRepository;
            _fileService = fileService;
        }

        public async Task<Result<FileStream>> Handle(GetLessonStreamQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken);

            if (lesson == null || string.IsNullOrEmpty(lesson.URL?.Value))
                return Result.Failure<FileStream>(LessonsErrors.NotFound);

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
