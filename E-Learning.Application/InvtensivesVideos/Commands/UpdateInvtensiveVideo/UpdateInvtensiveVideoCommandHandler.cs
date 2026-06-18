using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.InvtensivesVideos;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Commands.UpdateInvtensiveVideo
{
    public sealed class UpdateInvtensiveVideoCommandHandler : BaseService, ICommandHandler<UpdateInvtensiveVideoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;
        private readonly IFileService _fileService;

        public UpdateInvtensiveVideoCommandHandler(IUnitOfWork unitOfWork, 
            IUserRepository userRepository,
            IInvtensivesVideosRepositry invtensivesVideosRepo, 
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _invtensivesVideosRepo = invtensivesVideosRepo;
            _fileService = fileService;
        }

        public async Task<Result<Guid>> Handle(UpdateInvtensiveVideoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var invtensiveVideo = await _invtensivesVideosRepo.GetByIdAsync(request.Id, cancellationToken);
            if (invtensiveVideo is null)
                return Result.Failure<Guid>(InvtensivesVideosErrors.NotFound);
            if (invtensiveVideo.Invtensive.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            string url = invtensiveVideo.VideoUrl.Value;
            if (request.VideoUrl is not null)
            {
                if (!string.IsNullOrEmpty(invtensiveVideo.VideoUrl?.Value))
                    _fileService.DeleteVideo(invtensiveVideo.VideoUrl.Value);
                url = await _fileService.UploadVideoAsync(request.VideoUrl, "InvtensiveVideos", cancellationToken);
            }
            invtensiveVideo.UpdateVideo(
                new InvtensivesVideosVideoUrl(url)
            );
            await _invtensivesVideosRepo.UpdateAsync(invtensiveVideo , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(invtensiveVideo.Id);
        }
    }
}
