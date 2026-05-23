using E_Learning.Domain.ExamVideos;

namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class ExamVideoRepository : Repository<ExamVideo>, IExamVideoRepository
    {
        public ExamVideoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
