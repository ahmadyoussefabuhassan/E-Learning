

using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.ExamVideos.Commands.UpdateExamVideo
{
    public sealed class UpdateExamVideoCommandHandler : BaseService, ICommandHandler<UpdateExamVideoCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExamVideoRepository _videoRepository;
        private readonly IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateExamVideoCommandHandler(IUserRepository userRepository, 
            IExamVideoRepository videoRepository,
            IFileService fileService,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _videoRepository = videoRepository;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateExamVideoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId , cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var examVido = await _videoRepository.GetByIdAsync(request.Id , cancellationToken);
            if(examVido is null)
                return Result.Failure<Guid>(ExamVideosErrors.NotFound);
            if(examVido.ExamExplanation.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            string url = examVido.VideoUrl.Value;
            if(request.VidoUrl is not null)
            {
                if (!string.IsNullOrEmpty(examVido.VideoUrl?.Value))
                    _fileService.DeleteVideo(examVido.VideoUrl.Value);
                url = await _fileService.UploadVideoAsync(request.VidoUrl , "Exams" , cancellationToken);
            }
            examVido.UpdateVidoe(
                new ExamVideosVideoUrl(url),
                new Year(request.Year),
                new TitleVideoUrl(request.TitleUrl)
            );
            await _videoRepository.UpdateAsync(examVido , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(examVido.Id);
        }
    }
}
