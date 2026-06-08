namespace E_Learning.Api.Controllers.Student
{
    public sealed record LoginStudentRequest(
       string Email,
       string Password
    );
}
