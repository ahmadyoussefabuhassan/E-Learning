using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Courses
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllByClasses(Guid classId, CancellationToken cancellationToken = default);
    }
}
