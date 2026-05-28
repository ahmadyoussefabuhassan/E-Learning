

namespace E_Learning.Application.Users.LogInUser
{
    public sealed record AuthenticationResponse(
        string Token,
        string RefreshToken,
        Guid userId
    );
}
