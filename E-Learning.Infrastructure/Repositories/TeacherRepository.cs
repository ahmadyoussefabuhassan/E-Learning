using E_Learning.Domain.Teachers;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Teacher>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _dbContext.Set<Teacher>()
            .Include(t => t.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        public async Task<int> GetCountTeachersAsync(CancellationToken cancellation)
            => await _dbContext.Set<Teacher>()
            .CountAsync(t => t.User.Role.notType == Domain.Roles.NotType.Teacher, cancellation);

        public async Task<bool> HasActiveCoursesAsync(Guid teacherId, CancellationToken cancellationToken)
            => await _dbContext.Set<Teacher>()
            .Include(u => u.User)
            .ThenInclude(t => t.Courses)
            .AnyAsync(t => t.Id == teacherId && t.User.Courses.Any(c => c.IsActive), cancellationToken);
        public override async Task<Teacher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbContext.Set<Teacher>()
            .Include(u => u.User)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}
