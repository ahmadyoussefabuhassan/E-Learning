using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.ExamVideos
{
    public interface IExamVideoRepository : IRepository<ExamVideo>
    {
        Task<IEnumerable<ExamVideo>> GetAllByExamAsync(Guid ExamId, CancellationToken cancellation = default);
    }
    
}
