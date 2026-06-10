using E_Learning.Application.Classes.Commands.AddClass;
using E_Learning.Application.Classes.Commands.DeleteClass;
using E_Learning.Application.Classes.Commands.UpdateClass;
using E_Learning.Application.Classes.Queries.GetAllClass;
using E_Learning.Application.Classes.Queries.GetClassById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.Classes
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly ISender _sender;

        public ClassesController(ISender sender)
            => _sender = sender;
        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetClassById( [FromQuery]Guid id , CancellationToken cancellation)
        {
            var query = new GetClassByIdQuery(id);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAllClasses(CancellationToken cancellation)
        {
            var query = new GetAllClassesQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("AddClass")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddClass([FromBody]AddClassRequest request, CancellationToken cancellation = default)
        {
            var command = new AddClassCommand(request.Name);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateClass/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateClass([FromQuery] Guid id,[FromBody] UpdateClassRequest request, CancellationToken cancellation)
        {
            var command = new UpdateClassCommand(id, request.Name);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteClass/{id:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteClass([FromQuery] Guid Id,CancellationToken cancellation = default)
        {
            var command = new DeleteClassCommand(Id);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);

        }
    }
}
