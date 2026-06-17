using E_Learning.Application.ExamExplanations.Commands.AddExamExplanation;
using E_Learning.Application.ExamExplanations.Commands.DeleteExamExplanation;
using E_Learning.Application.ExamExplanations.Commands.UpdateExamExplanation;
using E_Learning.Application.ExamExplanations.Queries.GetAllExamExplanationByCourse;
using E_Learning.Application.ExamExplanations.Queries.GetExamExplanationById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.ExamExplanations
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamExplanationsController : ControllerBase
    {
        private readonly ISender _sender;

        public ExamExplanationsController(ISender sender)
            => _sender = sender;
        [HttpPost("AddExamExplanation/{courseId:guid}")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> AddExamExplanation(Guid courseId , [FromBody] AddExamExplanationRequest request, CancellationToken cancellationToken)
        {
            var command = new AddExamExplanationCommand(
                courseId,
                request.Title,
                request.Description,
                request.Price
            );
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateExamExplanation/{examId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateExamExplanation(Guid examId , [FromBody] UpdateExamExplanationRequest request , CancellationToken cancellation)
        {
            var command = new UpdateExamExplanationCommand(
                examId,
                request.Title,
                request.Description,
                request.Price
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteExamExplanation/{examId:guid}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> DeleteExamExplanation(Guid examId , CancellationToken cancellation)
        {
            var command = new DeleteExamExplanationCommand(examId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/{courseId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllExamExplanation(Guid courseId , CancellationToken cancellation)
        {
            var query = new GetAllExamExplanationByCourseQuery(courseId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetById/{examId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetExamExplanationById(Guid examId , CancellationToken cancellation)
        {
            var query = new GetExamExplanationByIdQuery(examId);
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
