using E_Learning.Application.Students.GetProfileStudent;
using E_Learning.Application.Students.LogInStudent;
using E_Learning.Application.Students.RegisterStudent;
using E_Learning.Application.Students.UpdateProfileStudent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ISender _sender;
        public StudentController(ISender sender)
            => _sender = sender;
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request, CancellationToken cancellation)
        {
            var command = new RegisterStudentCommand(
                request.FullName,
                request.Email,
                request.Password,
                request.PhoneNumber,
                request.Address,
                request.Education
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
       [HttpPost("Login")]
       [AllowAnonymous]
       public async Task<IActionResult> Login([FromBody] LoginStudentRequest request, CancellationToken cancellation)
       {
           var command = new LogInStudentCommand(
               request.Email,
               request.Password
           );
           var result = await _sender.Send(command, cancellation);
           return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
       }
       [HttpGet("Profile")]
       [Authorize(Roles = "Student")]
       public async Task<IActionResult> GetProfile(CancellationToken cancellation)
       {
            var query = new GetProfileStudentQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
       }
      [HttpPut("UpdateProfile")]
      [Authorize(Roles = "Student")]
      public async Task<IActionResult> UpdateProfile([FromForm] UpdateProfileStudentRequest request, CancellationToken cancellation)
      {
         var command = new UpdateProfileStudentCommand(
                request.FullName,
                request.Email,
                request.PhoneNumber,
                request.Address,
                request.ImageUrl,
                request.Education

         );
         var result = await _sender.Send(command, cancellation);
         return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
      }

    }

}
