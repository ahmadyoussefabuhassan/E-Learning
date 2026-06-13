namespace E_Learning.Api.Controllers.User
{
    public sealed record ChangePasswordResetCodeRequest(string code, string Password);
}
