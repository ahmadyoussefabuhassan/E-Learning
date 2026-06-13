namespace E_Learning.Api.Controllers.User
{
    public sealed record ChangePasswordRequest(
        string OldPassword,
        string NewPassword,
        string ChekPassword
    );
}
