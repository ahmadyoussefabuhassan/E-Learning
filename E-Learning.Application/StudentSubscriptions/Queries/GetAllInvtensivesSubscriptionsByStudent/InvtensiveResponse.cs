namespace E_Learning.Application.StudentSubscriptions.Queries.GetAllInvtensivesSubscriptionsByStudent
{
    public sealed record InvtensiveResponse(
          Guid Id,
         string Title,
         string Description,
         decimal Price,
         bool IsLocked
    );
}
