
namespace E_Learning.Application.Students.GetProfileStudent
{
    public record StudentResponse(
        string FullName,
        string Email,
        string PhoneNumber,
        string Address,
        string Imageurl,
        string Education,
        string RoleName
    );
    
}
