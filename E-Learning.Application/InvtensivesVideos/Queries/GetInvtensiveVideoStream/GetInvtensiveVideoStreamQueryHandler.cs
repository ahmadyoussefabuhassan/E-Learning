using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.InvtensivesVideos;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Queries.GetInvtensiveVideoStream
{
    public sealed class GetInvtensiveVideoStreamQueryHandler : BaseService,IQueryHandler<GetInvtensiveVideoStreamQuery, FileStream>
    {
        private readonly IFileService _fileService;
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;
        private readonly IUserRepository _userRepository;

        public GetInvtensiveVideoStreamQueryHandler(IFileService fileService, IInvtensivesVideosRepositry invtensivesVideosRepo, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : base(httpContextAccessor)
        {
            _fileService = fileService;
            _invtensivesVideosRepo = invtensivesVideosRepo;
            _userRepository = userRepository;
        }

        public async Task<Result<FileStream>> Handle(GetInvtensiveVideoStreamQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId , cancellationToken);
            if (user is null)
                return Result.Failure<FileStream>(UserErrors.NotFound);
            var invtensiveVideo = await _invtensivesVideosRepo.GetByIdAsync(request.Id, cancellationToken);
            if (invtensiveVideo is null)
                return Result.Failure<FileStream>(InvtensivesVideosErrors.NotFound);
            if(user.Role.notType == Domain.Roles.NotType.Student)
            {
                if (invtensiveVideo.Invtensive.IsLocked)
                    return Result.Failure<FileStream>(InvtensivesVideosErrors.AccessDenied);
            }
           
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
