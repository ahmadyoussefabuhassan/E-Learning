using E_Learning.Application.StudentSubscriptions.Commands.AcceptStudent;
using E_Learning.Application.StudentSubscriptions.Commands.RegisterCourse;
using E_Learning.Application.StudentSubscriptions.Commands.RegisterInvtensive;
using E_Learning.Application.StudentSubscriptions.Commands.RegisterSection;
using E_Learning.Application.StudentSubscriptions.Commands.RegiterExamExplanation;
using E_Learning.Application.StudentSubscriptions.Commands.RejecetStudent;
using E_Learning.Application.StudentSubscriptions.Queries.GetAllStudentSubscriptions;
using E_Learning.Application.StudentSubscriptions.Queries.GetStudentSubscriptionById;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using E_Learning.Application.StudentSubscriptions.Queries.GetAllCoursesSubscriptionsByStudent;
using E_Learning.Application.StudentSubscriptions.Queries.GetAllSectionsSubscriptionsByStudent;
using E_Learning.Application.StudentSubscriptions.Queries.GetAllExamExplanationsSubscriptionsByStudent;
using E_Learning.Application.StudentSubscriptions.Queries.GetAllInvtensivesSubscriptionsByStudent;

namespace E_Learning.Api.Controllers.StudentSubscriptions
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubscriptionsController : ControllerBase
    {
        private readonly ISender _sender;

        public StudentSubscriptionsController(ISender sender)
            => _sender = sender;
        [HttpPost("RegisterCourse/{courseId:guid}")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> RegisterCourse(Guid courseId , [FromForm] RegisterStudentSubscriptionsRequests request , CancellationToken cancellation)
        {
            var command = new RegisterCourseCommand(courseId , request.Image);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("RegisterInvtensive/{invtensiveId:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RegisterInvtensive(Guid invtensiveId , [FromForm] RegisterStudentSubscriptionsRequests request, CancellationToken cancellation)
        {
            var command = new RegisterInvtensiveCommand(invtensiveId , request.Image);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("RegisterSection/{sectionId:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RegisterSection(Guid sectionId , [FromForm] RegisterStudentSubscriptionsRequests request, CancellationToken cancellation)
        {
            var command = new RegisterSectionCommand(sectionId , request.Image);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("RegisterExam/{examId:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> RegisterExam(Guid examId, [FromForm] RegisterStudentSubscriptionsRequests request, CancellationToken cancellation)
        {
            var command = new RegiterExamExplanationCommand(examId , request.Image);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("Accept/{subscriptionId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptStudent(Guid subscriptionId , CancellationToken cancellation)
        {
            var command = new AcceptSubscriptionCommand(subscriptionId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("Rejecet/{subscriptionId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejecetStudent(Guid subscriptionId , CancellationToken cancellation)
        {
            var command = new RejectSubscriptionCommand(subscriptionId);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? status = null)
        {
            var query = new GetAllStudentSubscriptionsQuery(pageNumber, pageSize, status);
            var result = await _sender.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("GetById/{subscriptionId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid subscriptionId, CancellationToken cancellationToken)
        {
            var query = new GetStudentSubscriptionByIdQuery(subscriptionId);
            var result = await _sender.Send(query , cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/Courses")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllCoursesSubscription(CancellationToken cancellationToken)
        {
            var query = new GetAllCoursesSubscriptionsByStudentQuery();
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/Sections")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllSectionsSubscription(CancellationToken cancellationToken)
        {
            var query = new GetAllSectionsSubscriptionsByStudentQuery();
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/Exams")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllExamsSubscription(CancellationToken cancellationToken)
        {
            var query = new GetAllExamExplanationsSubscriptionsByStudentQuery();
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAll/Invtensives")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllInvtensivesSubscription(CancellationToken cancellationToken)
        {
            var query = new GetAllInvtensivesSubscriptionsByStudentQuery();
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
