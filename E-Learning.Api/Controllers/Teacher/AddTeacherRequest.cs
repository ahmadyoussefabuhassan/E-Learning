namespace E_Learning.Api.Controllers.Teacher
{
    public sealed record AddTeacherRequest(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber,
        string Address,
        string Education,
        string SahmCash
    );
}
