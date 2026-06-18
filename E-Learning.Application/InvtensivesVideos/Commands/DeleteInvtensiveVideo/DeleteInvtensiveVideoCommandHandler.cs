using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.InvtensivesVideos;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Commands.DeleteInvtensiveVideo
{
    public sealed class DeleteInvtensiveVideoCommandHandler : BaseService, ICommandHandler<DeleteInvtensiveVideoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;
        private readonly IFileService _fileService;

        public DeleteInvtensiveVideoCommandHandler(
            IUnitOfWork unitOfWork,
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

        public async Task<Result<bool>> Handle(DeleteInvtensiveVideoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            var invtensiveVideo = await _invtensivesVideosRepo.GetByIdAsync(request.Id, cancellationToken);
            if(invtensiveVideo is null)
                return Result.Failure<bool>(InvtensivesVideosErrors.NotFound);
            if (invtensiveVideo.Invtensive.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<bool>(UserErrors.Unauthorized);
            if (invtensiveVideo.VideoUrl is not null)
                _fileService.DeleteVideo(invtensiveVideo.VideoUrl.Value);
            await _invtensivesVideosRepo.DeleteAsync(invtensiveVideo.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
