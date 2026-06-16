using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Lessons
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetLessonsAsyncByUnitAsync(Guid unitId , CancellationToken cancellationToken);

    }
}
