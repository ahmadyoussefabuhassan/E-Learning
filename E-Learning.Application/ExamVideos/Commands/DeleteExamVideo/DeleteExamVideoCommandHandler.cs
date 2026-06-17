using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.ExamVideos.Commands.DeleteExamVideo
{
    public sealed class DeleteExamVideoCommandHandler : BaseService,ICommandHandler<DeleteExamVideoCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExamVideoRepository _examVideoRepository;
        private readonly IFileService _fileService;

        public DeleteExamVideoCommandHandler(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IExamVideoRepository examVideoRepository,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _examVideoRepository = examVideoRepository;
            _fileService = fileService;
        }

        public async Task<Result> Handle(DeleteExamVideoCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure(UserErrors.NotFound);
            var examVido = await _examVideoRepository.GetByIdAsync(request.Id, cancellationToken);
            if (examVido is null)
                return Result.Failure(ExamVideosErrors.NotFound);
            if (examVido.ExamExplanation.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure(UserErrors.Unauthorized);
            if (examVido.VideoUrl != null)
                _fileService.DeleteVideo(examVido.VideoUrl.Value);
            await _examVideoRepository.DeleteAsync(examVido.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
