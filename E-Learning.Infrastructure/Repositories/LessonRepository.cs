using E_Learning.Domain.Lessons;


namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
