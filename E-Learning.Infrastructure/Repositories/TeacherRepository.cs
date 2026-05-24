using E_Learning.Domain.Teachers;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
