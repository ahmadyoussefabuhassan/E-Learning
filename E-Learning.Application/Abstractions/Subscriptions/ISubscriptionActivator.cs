using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Application.Abstractions.Subscriptions
{
    public interface ISubscriptionActivator
    {
        bool CanHandle(string targetType);
        Task ActivateAsync(Guid targetId, CancellationToken ct);
    }
}
