using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Courses
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllByClassesAsync(Guid classId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Course>> GetAllByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default);
        Task UpdateLoukedSectionAsync(Guid courseId, CancellationToken cancellationToken = default);
    }
}
