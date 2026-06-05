using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Teachers.Events;

namespace E_Learning.Domain.Teachers
{
    public sealed class Teacher : Entity
    {
        private Teacher() : base(Guid.Empty)
        {
        }

        private Teacher(Guid Id, UrlShamCash urlShamCash, SubjectTeacher subjectTeacher) : base(Id)
        {
            UrlShamCash = urlShamCash;
            SubjectTeacher = subjectTeacher;
        }
        public UrlShamCash? UrlShamCash { get; private set; }
        public SubjectTeacher SubjectTeacher { get; private set; }
        public  ICollection<Course> Courses { get; private set; } = new List<Course>();
        public User.User User { get; private set; } = null!;
        public static Teacher Create(Guid userId, UrlShamCash urlShamCash , SubjectTeacher subjectTeacher)
        {
            var teacher = new Teacher(userId, urlShamCash ,subjectTeacher);
            teacher.RaiseDomainEvent(new TeacherCreatedDomainEvent(teacher.Id, teacher.UrlShamCash.Value, teacher.SubjectTeacher.Value));
            return teacher;
        }
        public void UpdateProfile(UrlShamCash urlShamCash, SubjectTeacher subjectTeacher)
        {
            UrlShamCash = urlShamCash;
            SubjectTeacher = subjectTeacher;
        }
    }
}
