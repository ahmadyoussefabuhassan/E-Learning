using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesForStudent
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
