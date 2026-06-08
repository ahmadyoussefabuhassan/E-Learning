using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Classes
{
    public interface IClassesRepositry : IRepository<Classes>
    {
        Task<Classes?> IsClassesUniqueAsync(ClassesName name ,CancellationToken cancellationToken);
        Task<Classes?> GetClassesByNameAsync(ClassesName name,CancellationToken cancellationToken);
        Task<bool> HasRelatedDataAsync(Guid classId, CancellationToken cancellationToken);
    }
}
