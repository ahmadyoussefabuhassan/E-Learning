using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllCoursesSubscriptionsByStudent
{
    public sealed record  GetAllCoursesSubscriptionsByStudentQuery : IQuery<IEnumerable<CourseResponse>>;
}
