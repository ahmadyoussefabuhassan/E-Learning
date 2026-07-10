namespace E_Learning.Api.Controllers.Teacher
{
    public sealed record LogInTeacherRequest(
        string Email,
        string Password
    );
}
