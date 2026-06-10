using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.Courses.Commands.AddCourse
{
    public sealed record AddCourseCommand(
        string Title,
        string Description,
        decimal Price,
        IFormFile ImageUrl,
        string ClassroomName
    ) : ICommand<Guid>;
}
