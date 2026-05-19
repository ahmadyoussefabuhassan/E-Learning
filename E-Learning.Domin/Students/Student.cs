using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Students
{
    public class Student : Entity
    {
        private Student() { }
        private Student(Guid Id) : base(Id) { }
    }
}
