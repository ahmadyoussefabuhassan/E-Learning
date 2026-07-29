using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.InvtensivesVideos;

namespace E_Learning.Application.InvtensivesVideos.Queries.GetAllInvtensivesVideosByInvtensive
{
    public sealed class GetAllInvtensivesVideosByInvtensiveQueryHandler : IQueryHandler<GetAllInvtensivesVideosByInvtensiveQuery, IEnumerable<InvtensiveVideoResponse>>
    {
        private readonly IInvtensivesRepositry _invtensivesRepositry;
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;

        public GetAllInvtensivesVideosByInvtensiveQueryHandler(
            IInvtensivesRepositry invtensivesRepositry,
            IInvtensivesVideosRepositry invtensivesVideosRepo)
        {
            _invtensivesRepositry = invtensivesRepositry;
            _invtensivesVideosRepo = invtensivesVideosRepo;
        }

        public async Task<Result<IEnumerable<InvtensiveVideoResponse>>> Handle(GetAllInvtensivesVideosByInvtensiveQuery request, CancellationToken cancellationToken)
        {
            var invtensive = await _invtensivesRepositry.GetByIdAsync(request.invtensiveId, cancellationToken);
            if (invtensive is null)
                return Result.Failure<IEnumerable<InvtensiveVideoResponse>>(InvtensivesErrors.NotFound);
            var invtensivesvideos = await _invtensivesVideosRepo.GetAllByInvtensiveAsync(invtensive.Id, cancellationToken);
            if(!invtensivesvideos.Any())
                return Result.Success(Enumerable.Empty<InvtensiveVideoResponse>());
            var rsponse = invtensivesvideos.Select(invVideo => new InvtensiveVideoResponse(
                invVideo.Id,
                invVideo.TitleVideoUrl.Value,
                invVideo.VideoUrl.Value
                
            ));
            return Result.Success(rsponse);
        }
    }
}
