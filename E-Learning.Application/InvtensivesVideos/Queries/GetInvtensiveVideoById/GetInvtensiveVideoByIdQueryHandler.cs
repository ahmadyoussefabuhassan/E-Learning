using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.InvtensivesVideos;

namespace E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoById
{
    public sealed class GetInvtensiveVideoByIdQueryHandler : IQueryHandler<GetInvtensiveVideoByIdQuery, InvtensiveVideoResponse>
    {
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;

        public GetInvtensiveVideoByIdQueryHandler(IInvtensivesVideosRepositry invtensivesVideosRepo)
        {
            _invtensivesVideosRepo = invtensivesVideosRepo;
        }

        public async Task<Result<InvtensiveVideoResponse>> Handle(GetInvtensiveVideoByIdQuery request, CancellationToken cancellationToken)
        {
            var invtensiveVideo = await _invtensivesVideosRepo.GetByIdAsync(request.Id, cancellationToken);
            if (invtensiveVideo is null)
                return Result.Failure<InvtensiveVideoResponse>(InvtensivesVideosErrors.NotFound);
            var response = new InvtensiveVideoResponse(
                invtensiveVideo.Id,
                invtensiveVideo.TitleVideoUrl.Value,
                $"/api/InvtensivesVideos/stream/{invtensiveVideo.Id}"
            );
            return Result.Success(response);
        }
    }
}
