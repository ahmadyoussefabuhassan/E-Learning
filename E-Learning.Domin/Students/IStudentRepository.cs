using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Students
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<int> GetCountStudentsAsync(CancellationToken cancellation);
    }
}
