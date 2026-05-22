using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Roles
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role?> GetByNameAsync(Name name, NotType type);
    }
}
