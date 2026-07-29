using E_Learning.Application.Sections.Commands.AddSection;
using E_Learning.Application.Sections.Commands.DeleteSection;
using E_Learning.Application.Sections.Commands.UpdateSection;
using E_Learning.Application.Sections.Queries.GetAllSectionsByCourse;
using E_Learning.Application.Sections.Queries.GetSectionById;
using E_Learning.Application.Sections.Queries.GetSectionByIdForStudent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_Learning.Api.Controllers.Sections
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISender _sender;

        public SectionsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("AddSection/{CourseId:guid}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> AddSection(Guid CourseId , [FromBody] AddSectionRequest request , CancellationToken cancellation)
        {
            var command = new AddSectionCommand(
                request.Title,
                request.Price,
                CourseId
            );
            var result =  await _sender.Send(command , cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateSection/{sectionId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateSection( Guid sectionId , [FromForm] UpdateSectionRequest request , CancellationToken cancellation)
        {
            var command = new UpdateSectionCommand(
                sectionId,
                request.Title,
                request.Price
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteSection/{sectionId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSection(Guid sectionId, CancellationToken cancellation)
        {
            var command = new DeleteSectionCommand(sectionId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAllSectionsByCourse/{CourseId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllSectionsByCourse( Guid CourseId, CancellationToken cancellation)
        {
            var query =  new GetAllSectionsByCourseQuery(CourseId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetSection/{sectionId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetSection(Guid sectionId , CancellationToken cancellation)
        {
            var query =  new GetSectionByIdQuery(sectionId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetSectionById/ForStudent/{sectionId:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetSectionByIdForStudent(Guid sectionId, CancellationToken cancellation)
        {
            var query = new GetSectionByIdForStudentQuery(sectionId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }


    }
}
