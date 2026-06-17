using E_Learning.Application.Lessons.Commands.AddLesson;
using E_Learning.Application.Lessons.Commands.DeleteLesson;
using E_Learning.Application.Lessons.Commands.UpdateLesson;
using E_Learning.Application.Lessons.Queries.GetAllLessonsByUnit;
using E_Learning.Application.Lessons.Queries.GetLessonById;
using E_Learning.Application.Lessons.Queries.GetLessonStream;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_Learning.Api.Controllers.Lessons
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ISender _sender;

        public LessonsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("stream/{lessonId:guid}")]
        [Authorize]
        public async Task<IActionResult> StreamVideo(Guid lessonId)
        {
            var result = await _sender.Send(new GetLessonStreamQuery(lessonId));

            if (result.IsFailure)
                return BadRequest(result.Error);
            return File(result.Value, "video/mp4", enableRangeProcessing: true);
        }
        [HttpPost("AddLesson/{unitId:guid}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> AddLesson(Guid unitId, [FromForm] AddLessonRequest request , CancellationToken cancellation)
        {
            var command = new AddLessonCommand(unitId , request.Title ,request.TitleUrl , request.VidoUrl);
            var result = await _sender.Send(command , cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateLesson/{lessonId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateLesson(Guid lessonId , [FromForm] UpdateLessonRequest request , CancellationToken cancellation)
        {
            var command = new UpdateLessonCommand(lessonId , request.Title ,request.TitleUrl , request.VidoUrl);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("{lessonId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteLesson(Guid lessonId , CancellationToken cancellation)
        {
            var command = new DeleteLessonCommand(lessonId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/{unitId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllLessons(Guid unitId , CancellationToken cancellation)
        {
            var query = new GetAllLessonsByUnitQuery(unitId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetById/{lessonId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetLesson(Guid lessonId , CancellationToken cancellation)
        {
            var query = new GetLessonByIdQuery(lessonId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

    }

}
