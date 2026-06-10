namespace E_Learning.Application.Students.Queries.GetProfileStudent
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
