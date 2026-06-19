

namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllStudentSubscriptions
{
    public sealed record StudentSubscriptionResponse(
        Guid Id,
        Guid StudentId,
        Guid targetId,
        string studentName ,
        string targetName, 
        string ImageUrl,
        string Status,
        decimal Price
    );
}
