using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.ExamExplanations.Commands.UpdateExamExplanation
{
    public sealed class UpdateExamExplanationCommandHandler : BaseService, ICommandHandler<UpdateExamExplanationCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExamExplanationRepository _examExplanationRepository;

        public UpdateExamExplanationCommandHandler(IUserRepository userRepository, 
            IUnitOfWork unitOfWork,
            IExamExplanationRepository examExplanationRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _examExplanationRepository = examExplanationRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateExamExplanationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(UserId , cancellationToken);
            if(user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var exam = await _examExplanationRepository.GetByIdAsync(request.Id , cancellationToken);
            if(exam is null)
                return Result.Failure<Guid>(ExamExplanationsErrors.NotFound);
            if(exam.Course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            exam.UpdateExam(
                new Title(request.Title),
                new Domain.Shared.Description(request.Description),
                new Domain.Shared.Price(request.Price)
            );
            return Result.Success(exam.Id);
        }
    }
}
