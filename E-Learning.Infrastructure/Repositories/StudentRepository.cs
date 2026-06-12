using E_Learning.Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> GetCountStudentsAsync(CancellationToken cancellation)
            => await _dbContext.Set<Student>()
                 .CountAsync(s => s.User.Role.notType == Domain.Roles.NotType.Student, cancellation);

    }
}
