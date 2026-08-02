using E_Learning.Application.Abstractions.Subscriptions;
using E_Learning.Domain.Invtensives;
using E_Learning.Domain.StudentSubscription;

namespace E_Learning.Application.Invtensives.Activator
{
    internal sealed class InvtensiveActivator : ISubscriptionActivator
    {
        private readonly IInvtensivesRepositry _invtensivesRepositry;

        public InvtensiveActivator(IInvtensivesRepositry invtensivesRepositry)
        {
            _invtensivesRepositry = invtensivesRepositry;
        }

        public bool CanHandle(string targetType)
            => targetType == TargetTypes.Invtensive.ToArabicString() ||
               targetType == TargetTypes.Invtensive.ToString();
        public async Task ActivateAsync(Guid targetId, CancellationToken ct)
        {
            var invtensive = await _invtensivesRepositry.GetByIdAsync(targetId);
            if (invtensive is null)
                return;
            if (invtensive is not null && invtensive.IsLocked)
            {
                invtensive.ToggleLock();
                await _invtensivesRepositry.UpdateAsync(invtensive , ct);
            }

        }


    }
}
