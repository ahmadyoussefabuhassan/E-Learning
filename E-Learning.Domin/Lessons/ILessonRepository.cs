using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Lessons
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetLessonsAsyncByUnit(Guid unitId , CancellationToken cancellationToken);

    }
}
