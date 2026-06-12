namespace E_Learning.Api.Controllers.Courses
{
    public sealed record AddCourseRequest(
        string Title,
        string Description,
        decimal Price,
        IFormFile ImageUrl,
        string ClassroomName
    );
}
