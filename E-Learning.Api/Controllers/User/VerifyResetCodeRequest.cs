namespace E_Learning.Api.Controllers.User
{
    public sealed record VerifyResetCodeRequest(string Email, string Code);
}
