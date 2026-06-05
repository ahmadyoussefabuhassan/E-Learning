namespace E_Learning.Api.Controllers.User
{
    public record LoginUserRequest(
        string Email,
        string Password
    );
}
