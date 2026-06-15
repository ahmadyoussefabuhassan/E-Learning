using E_Learning.Application.Units.Commands.AddUnit;
using E_Learning.Application.Units.Commands.DeleteUnit;
using E_Learning.Application.Units.Commands.UpdateUnit;
using E_Learning.Application.Units.Queries.GetAllUnitsBySection;
using E_Learning.Application.Units.Queries.GetUnitById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Learning.Api.Controllers.Units
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly ISender _sender;

        public UnitsController(ISender sender)
            => _sender = sender;
        [HttpGet("GetById/{unitId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetUnit(Guid unitId , CancellationToken cancellation)
        {
            var query =  new GetUnitByIdQuery(unitId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAllBySection/{sectionId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllUnitsBySection(Guid sectionId , CancellationToken cancellation)
        {
            var query =  new GetAllUnitsBySectionQuery(sectionId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("AddUnit/{sectionId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> AddUnit(Guid sectionId,[FromBody]AddUnitRequest request, CancellationToken cancellation)
        {
            var command =  new AddUnitCommand(request.Title,request.Description , sectionId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateUnit/{unitId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateUnit(Guid unitId , [FromBody] UpdateUnitRequest request, CancellationToken cancellation)
        {
            var command = new UpdateUnitCommand(unitId, request.Title, request.Description);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteUnit/{unitId:guid}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteUnit(Guid unitId , CancellationToken cancellation)
        {
            var command = new DeleteUnitCommand(unitId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
