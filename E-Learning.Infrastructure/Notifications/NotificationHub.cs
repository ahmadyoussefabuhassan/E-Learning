using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace E_Learning.Infrastructure.Notifications
{
    [Authorize]
    public sealed class NotificationHub : Hub
    {

    }
}
