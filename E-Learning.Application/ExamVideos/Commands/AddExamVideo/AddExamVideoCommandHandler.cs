using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.ExamVideos.Commands.AddExamVideo
{
    public sealed class AddExamVideoCommandHandler : BaseService, ICommandHandler<AddExamVideoCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExamExplanationRepository _examExplanationRepository;
        private readonly IExamVideoRepository _videoRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public AddExamVideoCommandHandler(IUserRepository userRepository,
            IExamExplanationRepository examExplanationRepository,
            IExamVideoRepository videoRepository,
            IFileService fileService, 
            IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor ) : base( httpContextAccessor ) 
        {
            _userRepository = userRepository;
            _examExplanationRepository = examExplanationRepository;
            _videoRepository = videoRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddExamVideoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if( user is null )
                return Result.Failure<Guid>(UserErrors.NotFound);
            var exam =  await _examExplanationRepository.GetByIdAsync(request.ExamId, cancellationToken);
            if ( exam is null )
                return Result.Failure<Guid>(ExamExplanationsErrors.NotFound); 
            if (exam.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            string vido = await _fileService.UploadVideoAsync(request.VidoUrl, "Exams", cancellationToken);
            var examvido = ExamVideo.Create(
              new ExamVideosVideoUrl(vido),
              new Year(request.Year),
              new TitleVideoUrl(request.TitleUrl),
              exam.Id
            );
            await _videoRepository.AddAsync( examvido , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(examvido.Id);

        }
    }
}
