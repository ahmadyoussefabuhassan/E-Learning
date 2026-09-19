namespace E_Learning.Application.Abstractions.Subscriptions
{
    public interface ISubscriptionActivator
    {
        bool CanHandle(string targetType);
        Task ActivateAsync(Guid targetId, CancellationToken ct);
    }
}
