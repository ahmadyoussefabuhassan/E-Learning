using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription;


namespace E_Learning.Application.StudentSubscriptions.Queries.GetStudentSubscriptionById
{
    public sealed class GetStudentSubscriptionByIdQueryHandler : IQueryHandler<GetStudentSubscriptionByIdQuery, StudentSubscriptionResponse>
    {
        private readonly IStudentSubscriptionRepositry _studentSubscriptionRepositry;

        public GetStudentSubscriptionByIdQueryHandler(IStudentSubscriptionRepositry studentSubscriptionRepositry)
        {
            _studentSubscriptionRepositry = studentSubscriptionRepositry;
        }

        public async Task<Result<StudentSubscriptionResponse>> Handle(GetStudentSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var studentSubscription = await _studentSubscriptionRepositry.GetByIdAsync(request.Id, cancellationToken);
            if (studentSubscription is null)
                return Result.Failure<StudentSubscriptionResponse>(StudentSubscriptionErrors.NotFound);
            var response = new StudentSubscriptionResponse(
                studentSubscription.Id,
                studentSubscription.StudentId,
                studentSubscription.TargetId,
                studentSubscription.Students.User.FullName.Value,
                studentSubscription.TargetType.Value,
                studentSubscription.ReceiptImageUrl.Value,
                studentSubscription.Status.ToString(),
                studentSubscription.PriceAtPurchase.Value
            );
            return Result.Success( response );

        }
    }
}
