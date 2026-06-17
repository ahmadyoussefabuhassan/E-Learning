using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Lessons
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetLessonsByUnitAsync(Guid unitId , CancellationToken cancellationToken);

    }
}
