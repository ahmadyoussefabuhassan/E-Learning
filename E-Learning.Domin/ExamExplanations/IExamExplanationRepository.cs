using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamExplanations
{
    public interface IExamExplanationRepository : IRepository<ExamExplanation>
    {
        Task<IEnumerable<ExamExplanation>> GetAllByCourseAsync(Guid courseId, CancellationToken cancellationToken);
    }
}
