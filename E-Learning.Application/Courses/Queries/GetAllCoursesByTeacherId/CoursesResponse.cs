
namespace E_Learning.Application.Courses.Queries.GetAllCoursesByTeacherId
{
    public sealed record CoursesResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        string ImageUrl,
        string ClassroomName
    );
}
