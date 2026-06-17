using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.ExamExplanations.Commands.AddExamExplanation
{
    public sealed class AddExamExplanationCommandHandler : BaseService, ICommandHandler<AddExamExplanationCommand, Guid>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IExamExplanationRepository _examExplanationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddExamExplanationCommandHandler(ICourseRepository courseRepository,
            IExamExplanationRepository examExplanationRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _courseRepository = courseRepository;
            _examExplanationRepository = examExplanationRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddExamExplanationCommand request, CancellationToken cancellationToken)
        {
            var user =  await _userRepository.GetByIdAsync(UserId , cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);
            var course = await _courseRepository.GetByIdAsync(request.CourseId , cancellationToken);
            if(course is null)
                return Result.Failure<Guid>(CourseErrors.NotFound);
            if (course.TeacherId != user.Id && user.Role.notType != Domain.Roles.NotType.Admin)
                return Result.Failure<Guid>(UserErrors.Unauthorized);
            var exam = ExamExplanation.Create(
                new Title(request.Title),
                new Domain.Shared.Description(request.Description),
                new Domain.Shared.Price(request.Price),
                course.Id
            );
            await _examExplanationRepository.AddAsync(exam, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(exam.Id);
        }
    }
}
