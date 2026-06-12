using E_Learning.Application.Abstractions.Messaging;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Courses.Commands.UpdateCourse
{
    public sealed record UpdateCourseCommand(
        Guid CourseId,
        string Title,
        string Description,
        decimal Price,
        IFormFile ImageFile, 
        string ClassroomName
    ): ICommand<Guid>;
}
