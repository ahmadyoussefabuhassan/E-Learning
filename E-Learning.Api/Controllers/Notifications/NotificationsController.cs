using E_Learning.Application.Notifications.Commands.DeleteAllNotifications;
using E_Learning.Application.Notifications.Commands.DeleteNotification;
using E_Learning.Application.Notifications.Commands.SendBroadcastNotification.E_Learning.Application.Notifications.Commands.SendBroadcastNotification;
using E_Learning.Application.Notifications.Queries.GetMyNotifications;
using E_Learning.Application.Notifications.Queries.GetNotificationById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ISender _sender;
        public NotificationsController(ISender sender) => _sender = sender;

        [HttpGet("my-notifications")]
        [Authorize]
        public async Task<IActionResult> GetMyNotifications()
        {
            var result = await _sender.Send(new GetMyNotificationsQuery());
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetNotificationByIdQuery(id));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("broadcast")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendBroadcast([FromBody] SendBroadcastNotificationRequest request , CancellationToken cancellation)
        {
            var command = new SendBroadcastNotificationCommand(request.Title, request.Message, request.Audience);
            var result = await _sender.Send(command , cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpDelete("delete-all")]
        [Authorize]
        public async Task<IActionResult> DeleteAllNotification(CancellationToken cancellation)
        {
            var command = new DeleteAllNotificationsCommand();
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteNotificationById(Guid id,CancellationToken cancellation)
        {
            var command = new DeleteNotificationCommand(id);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
