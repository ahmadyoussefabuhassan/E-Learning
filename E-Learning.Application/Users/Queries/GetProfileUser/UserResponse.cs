namespace E_Learning.Application.Users.Queries.GetProfileUser
{
    public sealed record UserResponse(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        string ImageUrl,
        string RoleName
    );
}
