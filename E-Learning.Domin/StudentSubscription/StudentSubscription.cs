using System;
using System;
using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.StudentSubscription
{
    public class StudentSubscription : Entity
    {
        private StudentSubscription() : base(Guid.Empty)
        {
        }

        private StudentSubscription(Guid id, Guid studentId, Guid targetId, string targetType, string receiptImageUrl, SubscriptionStatus status, decimal priceAtPurchase, DateTime createdAt)
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

        public Guid StudentId { get; private set; }
        public Guid TargetId { get; private set; }
        public string TargetType { get; private set; }
        public string ReceiptImageUrl { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public decimal PriceAtPurchase { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public static StudentSubscription Create(Guid studentId, Guid targetId, string targetType, string receiptImageUrl, SubscriptionStatus status , decimal priceAtPurchase , DateTime createdAt)
        {
            var subscription = new StudentSubscription(Guid.NewGuid(), studentId, targetId, targetType, receiptImageUrl, status, priceAtPurchase, createdAt);
            subscription .RaiseDomainEvent(new StudentSubscriptionCreatedEvent(subscription.Id, subscription.StudentId, subscription.TargetId, subscription.TargetType, subscription.ReceiptImageUrl, subscription.Status, subscription.PriceAtPurchase, subscription.CreatedAt));
            return subscription;
        }
    }

}
