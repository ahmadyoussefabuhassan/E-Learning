namespace E_Learning.Api.Controllers.User
{
    public sealed record LoginUserRequest(
        string Email,
        string Password
    );
}
