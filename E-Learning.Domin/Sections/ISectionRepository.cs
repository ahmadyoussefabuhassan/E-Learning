using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Sections
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<bool> HasRelatedDataAsync(Guid sectionId, CancellationToken cancellationToken);
        Task<IEnumerable<Section?>> GetAllByCourseAsync(Guid courseId, CancellationToken cancellationToken);
    }
}
