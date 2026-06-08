namespace E_Learning.Api.Controllers.Student
{
    public sealed record UpdateProfileStudentRequest(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile? ImageUrl,
        string Education
    );
}
