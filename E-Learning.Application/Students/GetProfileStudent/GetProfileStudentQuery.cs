using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Students.GetProfileStudent
{
    public record GetProfileStudentQuery() : IQuery<StudentResponse>;
}
