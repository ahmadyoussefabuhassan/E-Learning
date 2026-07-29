using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.ExamVideos.Queries.GetExamVideoStream
{
    public sealed class GetExamVideoStreamQueryHandler :  BaseService, IQueryHandler<GetExamVideoStreamQuery, FileStream>
    {
        private readonly IExamVideoRepository _repository;
        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;

        public GetExamVideoStreamQueryHandler(IExamVideoRepository repository, IFileService fileService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : base(httpContextAccessor)
        {
            _repository = repository;
            _fileService = fileService;
            _userRepository = userRepository;
        }

        public async Task<Result<FileStream>> Handle(GetExamVideoStreamQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId, cancellationToken);
            if (user is null)
                return Result.Failure<FileStream>(UserErrors.NotFound);
            var vidoe = await _repository.GetByIdAsync(request.examvideoId, cancellationToken);
            if (string.IsNullOrEmpty(vidoe?.VideoUrl.Value) || vidoe is null)
                return Result.Failure<FileStream>(ExamVideosErrors.NotFound);
            if(user.Role.notType == Domain.Roles.NotType.Student)
            {
                if (vidoe.ExamExplanation.IsLocked)
                    return Result.Failure<FileStream>(ExamVideosErrors.AccessDenied);
            }
        
            try
            {
                var stream = _fileService.GetVideoProvider(vidoe.VideoUrl.Value);
                return Result.Success(stream);
            }
            catch (FileNotFoundException)
            {
                return Result.Failure<FileStream>(ExamVideosErrors.FileNotFoundOnServer);
            }

        }
    }
}
