namespace E_Learning.Api.Controllers.User
{
    public sealed record UpdateProfileUserRequest(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        IFormFile ImageUrl
    );
}
