

namespace E_Learning.Application.Teachers.Queries.GetAllTeachers
{
    public sealed record TeachersResponse(
        Guid TeacherId,
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        string ImageUrl,
        string Education,
        string SahmCash
    );
}
