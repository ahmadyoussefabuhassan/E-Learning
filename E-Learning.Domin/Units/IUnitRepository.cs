using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Units
{
    public interface IUnitRepository : IRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetAllBySectionAsync(Guid sectionId , CancellationToken cancellationToken);
    }
}
