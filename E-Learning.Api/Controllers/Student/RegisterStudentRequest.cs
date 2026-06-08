namespace E_Learning.Api.Controllers.Student
{
    public sealed record RegisterStudentRequest(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,
        string Education
    );
}
