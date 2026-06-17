using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Lessons;


namespace E_Learning.Application.ExamVideos.Queries.GetExamVideoStream
{
    public sealed class GetExamVideoStreamQueryHandler : IQueryHandler<GetExamVideoStreamQuery, FileStream>
    {
        private readonly IExamVideoRepository _repository;
        private readonly IFileService _fileService;

        public GetExamVideoStreamQueryHandler(IExamVideoRepository repository, IFileService fileService)
        {
            _repository = repository;
            _fileService = fileService;
        }

        public async Task<Result<FileStream>> Handle(GetExamVideoStreamQuery request, CancellationToken cancellationToken)
        {
            var vidoe = await _repository.GetByIdAsync(request.examvideoId, cancellationToken);
            if (string.IsNullOrEmpty(vidoe?.VideoUrl.Value) || vidoe is null)
                return Result.Failure<FileStream>(ExamVideosErrors.NotFound);
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
