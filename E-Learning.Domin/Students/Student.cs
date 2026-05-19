using E_Learning.Domain.Abstractions;


namespace E_Learning.Domain.Students
{
    public class Student : Entity
    {
        private Student() : base(Guid.Empty)
        {
        }
        private Student(Guid Id, SubjectStudent subjectStudent) : base(Id)
        {
            SubjectStudent = subjectStudent;
        }
        public SubjectStudent SubjectStudent { get; private set; }
        public static Student Create(Guid userId, SubjectStudent subjectStudent)
        {
            var student = new Student(userId, subjectStudent);
            return student;
        }
    }
}
