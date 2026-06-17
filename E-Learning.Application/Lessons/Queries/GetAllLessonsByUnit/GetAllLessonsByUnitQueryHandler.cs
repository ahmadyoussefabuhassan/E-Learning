using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.Units;

namespace E_Learning.Application.Lessons.Queries.GetAllLessonsByUnit
{
    public sealed class GetAllLessonsByUnitQueryHandler : IQueryHandler<GetAllLessonsByUnitQuery, IEnumerable<LessonResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILessonRepository _lessonRepository;

        public GetAllLessonsByUnitQueryHandler(IUnitRepository unitRepository, ILessonRepository lessonRepository)
        {
            _unitRepository = unitRepository;
            _lessonRepository = lessonRepository;
        }

        public async Task<Result<IEnumerable<LessonResponse>>> Handle(GetAllLessonsByUnitQuery request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.unitId, cancellationToken);
            if (unit is null)
                return Result.Failure<IEnumerable<LessonResponse>>(UnitsErrors.NotFound);
            var lessons = await _lessonRepository.GetLessonsByUnitAsync(unit.Id, cancellationToken);
            if(!lessons.Any())
                return Result.Failure<IEnumerable<LessonResponse>>(LessonsErrors.NotFound);
            var response = lessons.Select(lesson => new LessonResponse(
                lesson.Id,
                lesson.LessonTitle.Value,
                lesson.TitleUrl.Value,
                lesson.URL.Value
            ));
            return Result.Success(response);
        }
    }
}
