using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesByTeacher
{
    public sealed class GetAllCoursesByTeacherQuery : PaginationRequest ,IQuery<GetAllDataResponse<CoursesResponse>>
    {
        public GetAllCoursesByTeacherQuery(
            int pageNumber = 1,
            int pageSize = 10,
            string? query = "" ,
            Guid? courseId = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Query = query;
            CourseId = courseId;
        }
        public Guid? CourseId { get; }
    }
}
