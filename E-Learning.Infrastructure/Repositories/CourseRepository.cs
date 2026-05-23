using E_Learning.Domain.Courses;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class CourseRepository : Repository<Course>, ICourseRepository 
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
