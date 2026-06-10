namespace E_Learning.Application.Users.Commands.LogIn
{
    public sealed record AuthenticationResponse(
        string Token,
        string RefreshToken,
        Guid userId
    );
}
