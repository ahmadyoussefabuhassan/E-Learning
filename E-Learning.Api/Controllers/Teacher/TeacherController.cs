using E_Learning.Application.Teachers.Commands.RegisterTeacher;
using E_Learning.Application.Teachers.Commands.UpdateProfileTeacher;
using E_Learning.Application.Teachers.Queries.GetProfileTeacher;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ISender _sender;
        public TeacherController(ISender sender)
            => _sender = sender;
        
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegisterTeacherRequest request, CancellationToken cancellation)
        {
            var command = new RegisterTeacherCommand(
                request.FullName,
                request.Email,
                request.Password,
                request.PhoneNumber,
                request.Address,
                request.Education,
                request.SahmCash
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("Profile")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetProfileTeacher(CancellationToken cancellation)
        {
            var query = new GetProfileTeacherQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdateProfile")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateProfileTeacher([FromForm] UpdateProfileTeacherRequest request, CancellationToken cancellation)
        {
            var command = new UpdateProfileTeacherCommand(
               request.FullName,
               request.Email,
               request.PhoneNumber,
               request.Address,
               request.ImageUrl,
               request.Education,
               request.SahmCash
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

    }
}
