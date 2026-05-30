

namespace E_Learning.Application.Students.LogInStudent
{
    public record AuthenticationResponse(
        string Token,
        string RefreshToken,
        Guid userId
    );
}
