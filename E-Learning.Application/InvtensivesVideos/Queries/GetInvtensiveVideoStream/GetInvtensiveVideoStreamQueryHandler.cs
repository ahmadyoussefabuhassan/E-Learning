using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.InvtensivesVideos;

namespace E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoStream
{
    public sealed class GetInvtensiveVideoStreamQueryHandler : IQueryHandler<GetInvtensiveVideoStreamQuery, FileStream>
    {
        private readonly IFileService _fileService;
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;

        public GetInvtensiveVideoStreamQueryHandler(IFileService fileService, IInvtensivesVideosRepositry invtensivesVideosRepo)
        {
            _fileService = fileService;
            _invtensivesVideosRepo = invtensivesVideosRepo;
        }

        public async Task<Result<FileStream>> Handle(GetInvtensiveVideoStreamQuery request, CancellationToken cancellationToken)
        {
            var invtensiveVideo = await _invtensivesVideosRepo.GetByIdAsync(request.Id, cancellationToken);
            if (invtensiveVideo is null)
                return Result.Failure<FileStream>(InvtensivesVideosErrors.NotFound);
            try
            {
                var stream = _fileService.GetVideoProvider(invtensiveVideo.VideoUrl.Value);
                return Result.Success(stream);
            }
            catch (FileNotFoundException)
            {
                return Result.Failure<FileStream>(InvtensivesVideosErrors.FileNotFoundOnServer);
            }
        }
    }
}
