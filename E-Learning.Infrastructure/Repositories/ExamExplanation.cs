using E_Learning.Domain.ExamExplanations;
namespace E_Learning.Infrastructure.Repositories
{
    internal sealed class ExamExplanationRepository : Repository<ExamExplanation>, IExamExplanationRepository
    {
        public ExamExplanationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

}