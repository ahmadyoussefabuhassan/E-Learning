
namespace E_Learning.Application.Teachers.Commands.LoginTeacher
{
    public sealed record AuthenticationResponse(
        string Token,
        string RefreshToken,
        Guid userId
    );
}
