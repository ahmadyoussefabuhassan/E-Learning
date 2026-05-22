using E_Learning.Domain.Abstractions;
using E_Learning.Domain.StudentSubscription.Events;

namespace E_Learning.Domain.StudentSubscription
{
    public sealed class StudentSubscription : Entity
    {
        private StudentSubscription() : base(Guid.Empty)
        {
        }

        private StudentSubscription(Guid id, Guid studentId, Guid targetId, TargetType targetType, ReceiptImageUrl receiptImageUrl, SubscriptionStatus status, PriceAtPurchase priceAtPurchase, DateTime createdAt)
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
        public TargetType TargetType { get; private set; }
        public ReceiptImageUrl ReceiptImageUrl { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public PriceAtPurchase PriceAtPurchase { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        public static StudentSubscription Create(Guid studentId, Guid targetId, TargetType targetType, ReceiptImageUrl receiptImageUrl, SubscriptionStatus status , PriceAtPurchase priceAtPurchase)
        {
            var subscription = new StudentSubscription(Guid.NewGuid(), studentId, targetId, targetType, receiptImageUrl, status, priceAtPurchase, DateTime.UtcNow);
            subscription.RaiseDomainEvent(new StudentSubscriptionCreatedEvent(subscription.Id, subscription.StudentId, subscription.TargetId, subscription.TargetType.Value, subscription.ReceiptImageUrl.Value, subscription.Status, subscription.PriceAtPurchase.Value, subscription.CreatedAt));
            return subscription;
        }
        public void Confirm()
        {
            if (Status == SubscriptionStatus.Pending)
            {
                Status = SubscriptionStatus.Completed;
                ExpiresAt = DateTime.UtcNow.AddYears(1); 

               RaiseDomainEvent(new SubscriptionConfirmedDomainEvent(Id, StudentId));
            }
        }
    }

}
