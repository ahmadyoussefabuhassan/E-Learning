using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;

namespace E_Learning.Application.ExamExplanations.Queries.GetExamExplanationById
{
    public sealed class GetExamExplanationByIdQueryHandler : IQueryHandler<GetExamExplanationByIdQuery, ExamExplanationResponse>
    {
        private readonly IExamExplanationRepository _examExplanationRepository;

        public GetExamExplanationByIdQueryHandler(IExamExplanationRepository examExplanationRepository)
        {
            _examExplanationRepository = examExplanationRepository;
        }

        public async Task<Result<ExamExplanationResponse>> Handle(GetExamExplanationByIdQuery request, CancellationToken cancellationToken)
        {
            var exam = await _examExplanationRepository.GetByIdAsync(request.examId , cancellationToken);
            if(exam is null)
                return Result.Failure<ExamExplanationResponse>(ExamExplanationsErrors.NotFound);
            var response = new ExamExplanationResponse(
                exam.Id,
                exam.Title.Value,
                exam.Description.Value,
                exam.Price.Value
            );
            return Result.Success( response );
        }
    }
}
