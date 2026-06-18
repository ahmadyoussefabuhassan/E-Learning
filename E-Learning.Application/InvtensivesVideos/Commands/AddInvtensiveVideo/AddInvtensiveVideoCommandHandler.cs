
using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.InvtensivesVideos;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Commands.AddInvtensiveVideo
{
    public sealed class AddInvtensiveVideoCommandHandler : BaseService, ICommandHandler<AddInvtensiveVideoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IInvtensivesRepositry _invtensivesRepositry;
        private readonly IInvtensivesVideosRepositry _invtensivesVideosRepo;
        private readonly IFileService _fileService;

        public AddInvtensiveVideoCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IInvtensivesRepositry invtensivesRepositry, 
            IInvtensivesVideosRepositry invtensivesVideosRepo,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _invtensivesRepositry = invtensivesRepositry;
            _invtensivesVideosRepo = invtensivesVideosRepo;
            _fileService = fileService;
        }

        public async Task<Result<Guid>> Handle(AddInvtensiveVideoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var invtensive = await _invtensivesRepositry.GetByIdAsync(request.invtensiveId, cancellationToken);
            if(invtensive is null)
                return Result.Failure<Guid>(InvtensivesErrors.NotFound);
           if (invtensive.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var vidoe = await _fileService.UploadVideoAsync(request.VidoeUrl, "InvtensiveVideos", cancellationToken);
            var invtensiveVideo = Domain.InvtensivesVideos.InvtensivesVideos.Create(
                invtensive.Id,
                new InvtensivesVideosVideoUrl(vidoe)
            );
            await _invtensivesVideosRepo.AddAsync(invtensiveVideo , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(invtensiveVideo.Id);

        }
    }
}
