using E_Learning.Domain.Courses;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class CourseRepository : Repository<Course>, ICourseRepository 
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _dbContext.Set<Course>()
            .AsNoTracking()
            .Include(c => c.Teachers)
            .Include(c => c.Classes)
            .ToListAsync(cancellationToken);

        public async Task<IEnumerable<Course>> GetAllByClasses(Guid classId, CancellationToken cancellationToken = default)
            =>  await _dbContext.Set<Course>()
            .AsNoTracking()
            .Include(c => c.Teachers)
            .Include(c => c.Classes)
            .Where(c => c.ClassesId == classId)
            .ToListAsync(cancellationToken);
        public override async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Course>()
            .AsNoTracking()
            .Include(c => c.Teachers)
            .Include(c => c.Classes)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
