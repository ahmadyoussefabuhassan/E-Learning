using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.ExamExplanations
{
    public interface IExamExplanationRepository : IRepository<ExamExplanation>
    {
        Task<IEnumerable<ExamExplanation>> GetAllByCourseAsync(Guid courseId, CancellationToken cancellationToken);
    }
}
