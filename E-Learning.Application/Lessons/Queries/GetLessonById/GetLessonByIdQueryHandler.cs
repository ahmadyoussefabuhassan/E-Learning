using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;

namespace E_Learning.Application.Lessons.Queries.GetLessonById
{
    public sealed class GetLessonByIdQueryHandler : IQueryHandler<GetLessonByIdQuery, LessonResponse>
    {
        private readonly ILessonRepository _lessonRepository;

        public GetLessonByIdQueryHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<Result<LessonResponse>> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.lessonId , cancellationToken);
            if (lesson is null)
                Result.Failure<LessonResponse>(LessonsErrors.NotFound);
            var response = new LessonResponse(
                lesson.Id,
                lesson.LessonTitle.Value,
                lesson.TitleUrl.Value,
                $"/api/Lessons/stream/{lesson.Id}"
            );
            return Result.Success(response);
        }
    }
}
