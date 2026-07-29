

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllCoursesSubscriptionsByStudent
{
    public sealed record CourseResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        string ImageUrl,
        string ClassroomName,
        string TeacherName,
        bool IsLocked
    );
}
