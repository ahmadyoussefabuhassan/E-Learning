using E_Learning.Domain.Courses;
using E_Learning.Domain.Sections;
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

        public async Task<IEnumerable<Course>> GetAllByClassesAsync(Guid classId, CancellationToken cancellationToken = default)
            =>  await _dbContext.Set<Course>()
            .AsNoTracking()
            .Include(c => c.Teachers)
            .Include(c => c.Classes)
            .Where(c => c.ClassesId == classId)
            .ToListAsync(cancellationToken);

        public async Task<IEnumerable<Course>> GetAllByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Course>()
            .AsNoTracking()
            .Include(c =>c.Classes)
            .Where(c => c.TeacherId == teacherId)
            .ToListAsync(cancellationToken);

        public override async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Course>()
            .Include(c => c.Teachers)
            .Include(c => c.Classes)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        public async Task UpdateLoukedSectionAsync(Guid courseId, CancellationToken cancellationToken = default)
        {
            var sections = await _dbContext.Set<Section>()
                 .Where(s => s.CourseId == courseId)
                .ToListAsync(cancellationToken);

            if (sections == null) return;

            foreach (var s in sections)
            {
                if (s.IsLocked)
                {
                    s.ToggleLock();
                }
            }
        }
    }
}
