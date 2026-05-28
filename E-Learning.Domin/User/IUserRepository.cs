using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.User
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);

    }
}
