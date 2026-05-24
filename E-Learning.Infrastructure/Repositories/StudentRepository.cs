using E_Learning.Domain.Students;


namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
