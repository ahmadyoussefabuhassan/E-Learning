using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;

namespace E_Learning.Application.ExamExplanations.Queries.GetExamExplanationByIdForStudent
{
    public sealed class GetExamExplanationByIdForStudentQueryHandler : IQueryHandler<GetExamExplanationByIdForStudentQuery, ExamExplanationResponse>
    {
        private readonly IExamExplanationRepository _examExplanationRepository;

        public GetExamExplanationByIdForStudentQueryHandler(IExamExplanationRepository examExplanationRepository)
        {
            _examExplanationRepository = examExplanationRepository;
        }

        public async Task<Result<ExamExplanationResponse>> Handle(GetExamExplanationByIdForStudentQuery request, CancellationToken cancellationToken)
        {
            var exam = await _examExplanationRepository.GetByIdAsync(request.Id, cancellationToken);
            if (exam is null)
                return Result.Failure<ExamExplanationResponse>(ExamExplanationsErrors.NotFound);
            var response = new ExamExplanationResponse(
                exam.Id,
                exam.Title.Value,
                exam.Description.Value,
                exam.Price.Value,
                exam.IsLocked
            );
            return Result.Success(response);
        }
    }
}
