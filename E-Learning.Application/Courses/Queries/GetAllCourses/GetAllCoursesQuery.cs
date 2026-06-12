using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Courses.Queries.SherdResponses;
using E_Learning.Domain.Abstractions;



namespace E_Learning.Application.Courses.Queries.GetAllCourses
{
    public sealed class GetAllCoursesQuery : PaginationRequest , IQuery<GetAllDataResponse<CourseResponse>>
    {
        public GetAllCoursesQuery(
            int pageNumber = 1,
            int pageSize = 10,
            Guid? teacherId = null,
            Guid? classId = null,
            Guid? courseId = null
        ) 
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TeacherId = teacherId;
            ClassId = classId;
            CourseId = courseId;
        }
       public Guid? TeacherId { get; }
        public Guid? ClassId { get; }
        public Guid? CourseId { get; }
    }
}
