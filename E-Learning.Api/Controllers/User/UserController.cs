using E_Learning.Application.Users.GetProfileUser;
using E_Learning.Application.Users.LogIn;
using E_Learning.Application.Users.LogOut;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Api.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
            => _sender = sender;
        [HttpPost("login/Admin")]
        public async Task<IActionResult> LoginAdman([FromBody] LoginUserRequest request, CancellationToken cancellation)
        {
            var command = new LogInUserCommand(request.Email, request.Password);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPost("LogOut")]
        [Authorize]
        public async Task<IActionResult> LogOut([FromHeader(Name = "X-Refresh-Token")] string refreshToken, CancellationToken cancellation)
        {
            var command = new LogOutUserCommand(refreshToken);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

        }
        [HttpGet("Profile/Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProfileAdman(CancellationToken cancellation)
        {
            var query = new GetProfileUserQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
