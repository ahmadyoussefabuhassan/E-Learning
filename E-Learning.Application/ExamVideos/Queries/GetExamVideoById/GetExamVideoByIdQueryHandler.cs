using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamVideos;

namespace E_Learning.Application.ExamVideos.Queries.GetExamVideoById
{
    public sealed class GetExamVideoByIdQueryHandler : IQueryHandler<GetExamVideoByIdQuery, ExamVidoeResponse>
    {
        private readonly IExamVideoRepository _examVideoRepository;

        public GetExamVideoByIdQueryHandler(IExamVideoRepository examVideoRepository)
        {
            _examVideoRepository = examVideoRepository;
        }

        public async Task<Result<ExamVidoeResponse>> Handle(GetExamVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var vidoe = await _examVideoRepository.GetByIdAsync(request.videoId , cancellationToken);
            if (vidoe is null) 
                return Result.Failure<ExamVidoeResponse>(ExamVideosErrors.NotFound);
            var response = new ExamVidoeResponse(
                vidoe.Id,
                $"/api/ExamVideos/stream/{vidoe.Id}",
                vidoe.Year.Value
            );
            return Result.Success( response );
        }
    }
}
