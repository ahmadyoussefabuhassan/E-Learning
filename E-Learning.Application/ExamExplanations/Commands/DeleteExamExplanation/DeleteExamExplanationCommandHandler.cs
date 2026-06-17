using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Lessons;

namespace E_Learning.Application.ExamExplanations.Commands.DeleteExamExplanation
{
    public sealed class DeleteExamExplanationCommandHandler : ICommandHandler<DeleteExamExplanationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExamExplanationRepository _examExplanationRepository;
        private readonly IExamVideoRepository _examVideoRepository;
        private readonly IFileService _fileService;

        public DeleteExamExplanationCommandHandler(IUnitOfWork unitOfWork,
            IExamExplanationRepository examExplanationRepository,
            IExamVideoRepository examVideoRepository,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _examExplanationRepository = examExplanationRepository;
            _examVideoRepository = examVideoRepository;
            _fileService = fileService;
        }

        public async Task<Result<bool>> Handle(DeleteExamExplanationCommand request, CancellationToken cancellationToken)
        {
            var exam = await _examExplanationRepository.GetByIdAsync(request.ExamId , cancellationToken);
            if (exam is null)
                return Result.Failure<bool>(ExamExplanationsErrors.NotFound);
            var examvidos = await _examVideoRepository.GetAllByExamAsync(exam.Id , cancellationToken);
            if (!examvidos.Any() && examvidos is not null)
            {
                foreach (var examvido in examvidos)
                {
                    if (!string.IsNullOrEmpty(examvido.VideoUrl?.Value))
                    {
                        _fileService.DeleteVideo(examvido.VideoUrl.Value);
                    }
                    await _examVideoRepository.DeleteAsync(examvido.Id, cancellationToken);
                }
            }
            await _examExplanationRepository.DeleteAsync(exam.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
