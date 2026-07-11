using E_Learning.Application.Courses.Commands.AddCourse;
using E_Learning.Application.Courses.Commands.DeleteCourse;
using E_Learning.Application.Courses.Commands.UpdateCourse;
using E_Learning.Application.Courses.Queries.GetAllCourses;
using E_Learning.Application.Courses.Queries.GetAllCoursesByTeacher;
using E_Learning.Application.Courses.Queries.GetAllCoursesFilterByClass;
using E_Learning.Application.Courses.Queries.GetAllCoursesForStudent;
using E_Learning.Application.Courses.Queries.GetCourseById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace E_Learning.Api.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ISender _sender;
        public CoursesController(ISender sender)
            => _sender = sender;
        [HttpPost("AddCourse")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> AddCourse([FromForm] AddCourseRequest request, CancellationToken cancellationToken)
        {
            var command = new AddCourseCommand(
                request.Title,
                request.Description,
                request.Price,
                request.ImageUrl,
                request.ClassroomName
            );
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateCourse/{id:guid}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> UpdateCourse(Guid id, [FromForm] UpdateCourseRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateCourseCommand(
                id,
                request.Title,
                request.Description,
                request.Price,
                request.ImageUrl,
                request.ClassroomName
            );
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpDelete("DeleteCourse/{id:guid}")]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> DeleteCourse(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteCourseCommand(id);
            var result = await _sender.Send(command, cancellationToken);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? teacherId = null,
            [FromQuery] Guid? classId = null,
            [FromQuery] Guid? courseId = null,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllCoursesQuery(pageNumber, pageSize, teacherId, classId, courseId);
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAllFilter/Student")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllFilterStudent(CancellationToken cancellation)
        {
            var query = new GetAllCoursesFilterByClassQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetById/{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCourseByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAllFilter/Teacher")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetAllByTeacher(CancellationToken cancellation)
        {
            var query = new GetAllCoursesByTeacherQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("GetAllFor/Search/Student")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetAllForStudent(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 100,
            [FromQuery] string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            var query = new GetAllCoursesForStudentQuery(pageNumber, pageSize, searchTerm);
            var result = await _sender.Send(query, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
