namespace E_Learning.Application.Students.Commands.LogInStudent
{
    public record AuthenticationResponse(
        string Token,
        string RefreshToken,
        Guid userId
    );
}
