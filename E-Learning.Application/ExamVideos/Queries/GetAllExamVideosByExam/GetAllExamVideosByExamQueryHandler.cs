using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;

namespace E_Learning.Application.ExamVideos.Queries.GetAllExamVideosByExam
{
    public sealed class GetAllExamVideosByExamQueryHandler : IQueryHandler<GetAllExamVideosByExamQuery, IEnumerable<ExamVidoeResponse>>
    {
        private readonly IExamExplanationRepository _examExplanationRepository;
        private readonly IExamVideoRepository _videoRepository;

        public GetAllExamVideosByExamQueryHandler(IExamExplanationRepository examExplanationRepository, IExamVideoRepository videoRepository)
        {
            _examExplanationRepository = examExplanationRepository;
            _videoRepository = videoRepository;
        }

        public async Task<Result<IEnumerable<ExamVidoeResponse>>> Handle(GetAllExamVideosByExamQuery request, CancellationToken cancellationToken)
        {
            var exam = await _examExplanationRepository.GetByIdAsync(request.ExamId , cancellationToken);
            if (exam is null)
                return Result.Failure<IEnumerable<ExamVidoeResponse>>(ExamExplanationsErrors.NotFound);
            var videos = await _videoRepository.GetAllByExamAsync(exam.Id, cancellationToken);
            if(!videos.Any())
                return Result.Success(Enumerable.Empty<ExamVidoeResponse>());
            var response = videos.Select(video => new ExamVidoeResponse(
              video.Id,
              video.VideoUrl.Value,
              video.Year.Value
            ));
            return Result.Success(response);
        }
    }
}
