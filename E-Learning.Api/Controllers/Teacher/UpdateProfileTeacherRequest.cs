namespace E_Learning.Api.Controllers.Teacher
{
    public sealed record UpdateProfileTeacherRequest(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile? ImageUrl,
        string Education,
        string SahmCash
    );
}
