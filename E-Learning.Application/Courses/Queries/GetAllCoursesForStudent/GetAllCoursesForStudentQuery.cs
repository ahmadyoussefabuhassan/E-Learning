using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Courses.Queries.SherdResponses;
using E_Learning.Domain.Abstractions;

namespace E_Learning.Application.Courses.Queries.GetAllCoursesForStudent
{
    public sealed class GetAllCoursesForStudentQuery : PaginationRequest, IQuery<GetAllDataResponse<CourseResponse>>
    {
        public GetAllCoursesForStudentQuery(
            int pageNumber = 1,
            int pageSize = 100, 
            string? searchTerm = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchTerm = searchTerm;
        }

        public string? SearchTerm { get; }
    }
}
