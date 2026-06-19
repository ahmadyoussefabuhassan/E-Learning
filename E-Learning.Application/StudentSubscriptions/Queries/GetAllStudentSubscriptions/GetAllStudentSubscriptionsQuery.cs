using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;


namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllStudentSubscriptions
{
    public sealed class GetAllStudentSubscriptionsQuery : PaginationRequest, IQuery<GetAllDataResponse<StudentSubscriptionResponse>>
    {
        public GetAllStudentSubscriptionsQuery(int pageNumber, int pageSize, string? status = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Status = status;
        }
        public string? Status { get; } 
    }
}
