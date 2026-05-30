

namespace E_Learning.Application.Users.LogIn
{
    public sealed record AuthenticationResponse(
        string Token,
        string RefreshToken,
        Guid userId
    );
}
