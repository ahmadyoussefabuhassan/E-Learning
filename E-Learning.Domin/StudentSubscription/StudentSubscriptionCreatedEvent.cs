using System;
using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.StudentSubscription
{
    public sealed record StudentSubscriptionCreatedEvent(Guid Id, Guid StudentId, Guid TargetId, string TargetType, string ReceiptImageUrl, SubscriptionStatus Status, decimal PriceAtPurchase, DateTime CreatedAt) : IDomainEvent;

}
