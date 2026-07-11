using E_Learning.Application.Abstractions.Extensions; 
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.StudentSubscriptions.Queries.GetAllStudentSubscriptions;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription;
using Microsoft.EntityFrameworkCore;

public sealed class GetAllStudentSubscriptionsQueryHandler : IQueryHandler<GetAllStudentSubscriptionsQuery, GetAllDataResponse<StudentSubscriptionResponse>>
{
    private readonly IStudentSubscriptionRepositry _subscriptionRepo;

    public GetAllStudentSubscriptionsQueryHandler(IStudentSubscriptionRepositry subscriptionRepo)
    {
        _subscriptionRepo = subscriptionRepo;
    }

    public async Task<Result<GetAllDataResponse<StudentSubscriptionResponse>>> Handle(GetAllStudentSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        var query = _subscriptionRepo.GetAllQueryableAsync()
            .Include(s => s.Students)
                .ThenInclude(st => st.User)
                .AsQueryable(); 
        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(s => s.Status.ToString() == request.Status);
        }
        var response = await query.ToPagedResponseAsync(
            request.PageNumber,
            request.PageSize,
            s => new StudentSubscriptionResponse(
                s.Id,
                s.StudentId,
                s.TargetId,
                s.Students.User.FullName.Value,
                s.TargetType.Value,
                s.ReceiptImageUrl.Value,
                s.Status.ToString(),
                s.PriceAtPurchase.Value
            )
        );

        return Result.Success(response);
    }
}