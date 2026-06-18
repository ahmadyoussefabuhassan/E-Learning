using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Invtensives
{
    public interface IInvtensivesRepositry : IRepository<Invtensives>   
    {
        Task<IEnumerable<Invtensives>> GetAllInvtensivesByCourseAsync(Guid courseId , CancellationToken cancellationToken);
    }
}
