namespace E_Learning.Application.Courses.Queries.SherdResponses
{
    public sealed record CourseResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        string ImageUrl,
        string ClassroomName,
        string TeacherName
    );
}
