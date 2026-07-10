using E_Learning.Application.Users.Commands.ChangePassword;
using E_Learning.Application.Users.Commands.ChangePasswordResetCode;
using E_Learning.Application.Users.Commands.ForgotPassword;
using E_Learning.Application.Users.Commands.LogIn;
using E_Learning.Application.Users.Commands.LogOut;
using E_Learning.Application.Users.Commands.UpdateProfileUser;
using E_Learning.Application.Users.Commands.VerifyResetCode;
using E_Learning.Application.Users.Queries.GetCountUsers;
using E_Learning.Application.Users.Queries.GetProfileUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPut("UpdateProfile/Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProfileAdman([FromForm] UpdateProfileUserRequest request, CancellationToken cancellation)
        {
            var command = new UpdateProfileUserCommand(
                request.FullName,
                request.Email,
                request.PhoneNumber,
                request.Address,
                request.ImageUrl
            );
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpPut("UpdatePassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] ChangePasswordRequest  request, CancellationToken cancellation)
        {
            var command = new ChangePasswordCommand(request.OldPassword, request.NewPassword, request.ChekPassword);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [HttpGet("Count")]
        [Authorize(Roles ="Admin,Teacher")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellation)
        {
            var query = new GetCountUsersQuery();
            var result = await _sender.Send(query, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody]SendResetCodeRequest request, CancellationToken cancellation)
        {
            var command = new SendResetCodeCommand(request.Email);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
        [AllowAnonymous]
        [HttpPost("verify-reset-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyResetCodeRequest request , CancellationToken cancellation)
        {
            var command = new VerifyResetCodeCommand(request.Email ,request.Code);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordResetCodeRequest request , CancellationToken cancellation)
        {
            var command = new ChangePasswordResetCodeCommand(request.code, request.Password);
            var result = await _sender.Send(command, cancellation);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
