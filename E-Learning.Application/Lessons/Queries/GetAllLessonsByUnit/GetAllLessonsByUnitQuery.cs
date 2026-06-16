using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Lessons.Queries.GetAllLessonsByUnit
{
    public sealed record GetAllLessonsByUnitQuery(Guid unitId) : IQuery<IEnumerable<LessonResponse>>;
}
