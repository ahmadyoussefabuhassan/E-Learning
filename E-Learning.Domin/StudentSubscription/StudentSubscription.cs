using System;
using System;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription.Events;

namespace E_Learning.Domain.StudentSubscription
{
    public class StudentSubscription : Entity
    {
        private StudentSubscription() : base(Guid.Empty)
        {
        }

        private StudentSubscription(Guid id, StudentId studentId, TargetId targetId, TargetType targetType, ReceiptImageUrl receiptImageUrl, SubscriptionStatus status, PriceAtPurchase priceAtPurchase, CreatedAt createdAt)
            : base(id)
        {
            StudentId = studentId;
            TargetId = targetId;
            TargetType = targetType;
            ReceiptImageUrl = receiptImageUrl;
            Status = status;
            PriceAtPurchase = priceAtPurchase;
            CreatedAt = createdAt;
        }

        public StudentId StudentId { get; private set; }
        public TargetId TargetId { get; private set; }
        public TargetType TargetType { get; private set; }
        public ReceiptImageUrl ReceiptImageUrl { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public PriceAtPurchase PriceAtPurchase { get; private set; }
        public CreatedAt CreatedAt { get; private set; }

        public static StudentSubscription Create(StudentId studentId, TargetId targetId, TargetType targetType, ReceiptImageUrl receiptImageUrl, SubscriptionStatus status , PriceAtPurchase priceAtPurchase , CreatedAt createdAt)
        {
            var subscription = new StudentSubscription(Guid.NewGuid(), studentId, targetId, targetType, receiptImageUrl, status, priceAtPurchase, createdAt);
            subscription .RaiseDomainEvent(new StudentSubscriptionCreatedEvent(subscription.Id, subscription.StudentId.Value , subscription.TargetId.Value , subscription.TargetType.Value, subscription.ReceiptImageUrl.Value, subscription.Status, subscription.PriceAtPurchase.Value, subscription.CreatedAt.Value));
            return subscription;
        }
    }

}
