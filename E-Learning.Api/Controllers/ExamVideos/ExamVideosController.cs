using E_Learning.Application.ExamVideos.Commands.AddExamVideo;
using E_Learning.Application.ExamVideos.Commands.DeleteExamVideo;
using E_Learning.Application.ExamVideos.Commands.UpdateExamVideo;
using E_Learning.Application.ExamVideos.Queries.GetAllExamVideosByExam;
using E_Learning.Application.ExamVideos.Queries.GetExamVideoById;
using E_Learning.Application.ExamVideos.Queries.GetExamVideoStream;
using E_Learning.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace E_Learning.Api.Controllers.ExamVideos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamVideosController : ControllerBase
    {
        private readonly ISender _sender;
        public ExamVideosController(ISender sender)
            => _sender = sender;
        [HttpGet("stream/{examvidoeId:guid}")]
        [Authorize]
        public async Task<IActionResult> StreamVideo(Guid examvidoeId)
        {
            var query = new GetExamVideoStreamQuery(examvidoeId);
            var result = await _sender.Send(query);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return File(result.Value, "video/mp4", enableRangeProcessing: true);
        }
        [HttpPost("AddExamVideo/{examId:guid}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> AddExamVideo(Guid examId, [FromForm] AddExamVideoRequest request , CancellationToken cancellation)
        {
            var command = new AddExamVideoCommand(
                examId,
                request.VidoUrl,
                request.Year
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateExamVideo/{examvidoeId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateExamVideo(Guid examvidoeId , [FromForm] UpdateExamVideoRequest request , CancellationToken cancellation)
        {
            var command = new UpdateExamVideoCommand(
                examvidoeId,
                request.VidoUrl,
                request.Year
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteExamVideo/{examvidoeId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteExamVideo(Guid examvidoeId , CancellationToken cancellation)
        {
            var command = new DeleteExamVideoCommand(examvidoeId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/{examId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllExamVideo(Guid examId , CancellationToken cancellation)
        {
            var query = new GetAllExamVideosByExamQuery(examId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [HttpGet("GetById/{examvidoeId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetExamVideo(Guid examvidoeId, CancellationToken cancellation)
        {
            var query = new GetExamVideoByIdQuery(examvidoeId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
