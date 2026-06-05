using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Students.Events;


namespace E_Learning.Domain.Students
{
    public sealed class Student : Entity
    {
        private Student() : base(Guid.Empty)
        {

        }
        private Student(Guid Id, SubjectStudent subjectStudent) : base(Id)
        {
            SubjectStudent = subjectStudent;
        }
        public SubjectStudent SubjectStudent { get; private set; }
        public ICollection<StudentSubscription.StudentSubscription> StudentSubscriptions { get; private set; } = new List<StudentSubscription.StudentSubscription>();
        public User.User User { get; private set; } = null!;
        public static Student Create(Guid userId, SubjectStudent subjectStudent)
        {
            var student = new Student(userId, subjectStudent);
            student.RaiseDomainEvent(new StudentCreatedDomainEvent(student.Id, student.SubjectStudent.Value));
            return student;
        }
        public void UpdateProfile(SubjectStudent subjectStudent)
            => SubjectStudent = subjectStudent;
    }
}
