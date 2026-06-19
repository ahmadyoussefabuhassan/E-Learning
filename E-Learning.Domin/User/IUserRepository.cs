using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;

namespace E_Learning.Domain.User
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
       
        Task<User?> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken);
        Task<User?> GetResetCodeAsync(string  resetCode, CancellationToken cancellationToken);
        Task<int> GetCountUserssAsync(CancellationToken cancellation);
        Task<List<Guid>> GetUserIdsByRoleAsync(NotType roleType, CancellationToken cancellationToken);
        Task<List<Guid>> GetAllUsersExceptAdminAsync(CancellationToken cancellationToken);
    }
}
