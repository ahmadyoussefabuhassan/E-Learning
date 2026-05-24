using E_Learning.Domain.Classes;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class ClassesRepositry : Repository<Classes>, IClassesRepositry
    {
        public ClassesRepositry(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
