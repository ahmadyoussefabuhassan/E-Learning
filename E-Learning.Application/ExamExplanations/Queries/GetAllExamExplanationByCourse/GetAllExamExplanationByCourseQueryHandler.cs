using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations;

namespace E_Learning.Application.ExamExplanations.Queries.GetAllExamExplanationByCourse
{
    public sealed class GetAllExamExplanationByCourseQueryHandler : IQueryHandler<GetAllExamExplanationByCourseQuery, IEnumerable<ExamExplanationResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IExamExplanationRepository _examExplanationRepository;

        public GetAllExamExplanationByCourseQueryHandler(ICourseRepository courseRepository, 
            IExamExplanationRepository examExplanationRepository)
        {
            _courseRepository = courseRepository;
            _examExplanationRepository = examExplanationRepository;
        }

        public async Task<Result<IEnumerable<ExamExplanationResponse>>> Handle(GetAllExamExplanationByCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.courseId , cancellationToken);
            if (course is null)
                return Result.Failure<IEnumerable<ExamExplanationResponse>>(CourseErrors.NotFound);
            var exams = await _examExplanationRepository.GetAllByCourseAsync(course.Id, cancellationToken);
            if(!exams.Any())
                return Result.Success(Enumerable.Empty<ExamExplanationResponse>());
            var response = exams.Select(exam => new ExamExplanationResponse(
                exam.Id,
                exam.Title.Value,
                exam.Description.Value,
                exam.Price.Value
            ));
            return Result.Success(response);
        }
    }
}
