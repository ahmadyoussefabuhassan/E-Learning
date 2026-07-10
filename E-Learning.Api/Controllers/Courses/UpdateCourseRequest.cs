namespace E_Learning.Api.Controllers.Courses
{
    public sealed record UpdateCourseRequest(
        string Title,
        string Description,
        decimal Price,
        IFormFile ImageUrl,
        string ClassroomName
    );
}
