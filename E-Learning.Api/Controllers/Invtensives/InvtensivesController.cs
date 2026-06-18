using E_Learning.Application.Invtensives.Commands.AddInvtensive;
using E_Learning.Application.Invtensives.Commands.DeleteInvtensive;
using E_Learning.Application.Invtensives.Commands.UpdateInvtensive;
using E_Learning.Application.Invtensives.Queries.GetAllInvtensivesByCourse;
using E_Learning.Application.Invtensives.Queries.GetInvtensivesById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.Invtensives
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvtensivesController : ControllerBase
    {
        private ISender _sender;

        public InvtensivesController(ISender sender)
            => _sender = sender;

        [HttpPost("AddInvtensive/{courseId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddInvtensive(Guid courseId, [FromBody] AddInvtensiveRequest request, CancellationToken cancellationToken)
        {
            var command = new AddInvtensiveCommand(
                courseId,
                request.Title,
                request.Description,
                request.Price
            );
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateInvtensive/{invtensiveId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateInvtensive(Guid invtensiveId, [FromBody] UpdateInvtensiveRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateInvtensiveCommand(
                invtensiveId,
                request.Title,
                request.Description,
                request.Price
            );
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteInvtensive/{invtensiveId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteInvtensive(Guid invtensiveId, CancellationToken cancellationToken)
        {
            var command = new DeleteInvtensiveCommand(invtensiveId);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/{courseId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllInvtensive(Guid courseId, CancellationToken cancellation)
        {
            var query = new GetAllInvtensivesByCourseQuery(courseId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetById/{invtensiveId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetInvtensive(Guid invtensiveId, CancellationToken cancellation)
        {
            var query = new GetInvtensivesByIdQuery(invtensiveId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
